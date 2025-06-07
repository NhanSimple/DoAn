const board = document.querySelector(".chessboard");
const squareSize = 40;
let boardState = getInitialBoardState();
let offsetX = 0
let offsetY = 0;
let startX = null;
let startY = null;
let selectedPiece = null;
let fen = boardStateToFEN(getInitialBoardState(), 0);
let move = null;
let turn = 0;
let currentTurn = 'w';
let draggedPiece = null;
const scale = 0.8;
const chessSize = squareSize * scale;
const offset = (squareSize - chessSize) / 2;// khoảng cách từ ô tới quân cờ 
const offsetCenter = chessSize / 2;




function drawBoard() {
    for (let row = 0; row < 8; row++) {
        for (let col = 0; col < 8; col++) {
            const squareName = coordsToSquare(row, col);
            const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");

            rect.setAttribute("x", col * squareSize);
            rect.setAttribute("y", row * squareSize);
            rect.setAttribute("width", squareSize);
            rect.setAttribute("height", squareSize);
            rect.classList.add((row + col) % 2 === 0 ? "white-square" : "black-square");

            rect.setAttribute("id", squareName);
            rect.dataset.square = squareName;
            rect.dataset.row = row;
            rect.dataset.col = col;
            board.appendChild(rect);
        }
    }
}

function renderChess(state) {
    // Xóa quân cờ hiện tại
    const oldPieces = board.querySelectorAll(".piece");
    oldPieces.forEach(p => p.remove());

    // Vẽ lại quân cờ mới
    for (let row = 0; row < 8; row++) {
        for (let col = 0; col < 8; col++) {
            const piece = state[row][col];
            if (!piece) continue;

            const img = document.createElementNS("http://www.w3.org/2000/svg", "image");
            img.setAttribute("href", `/Content/Images/${piece}.svg`);
            img.setAttribute("x", col * squareSize + offset);
            img.setAttribute("y", row * squareSize + offset);
            img.setAttribute("width", chessSize);
            img.setAttribute("height", chessSize);
            img.setAttribute("class", "piece");
            img.setAttribute("id", pieceMap[piece]);
            img.dataset.row = row;
            img.dataset.col = col;

            //chuột xuống 
            img.addEventListener("mousedown", (e) => {
                draggedPiece = e.target;
                if (draggedPiece) {
                    //const square = document.querySelector(`rect[data-row="${draggedPiece.dataset.row}"][data-col="${draggedPiece.dataset.col}"]`);

                    //if (square) {
                    //    square.classList.add("highlight-origin"); // hoặc đổi màu, lấy tọa độ, xử lý gì cũng được
                    //}
                    draggedPiece.classList.add("dragging");
                    board.classList.add("dragging-active");
                    startX = parseFloat(draggedPiece.getAttribute("x"));
                    startY = parseFloat(draggedPiece.getAttribute("y"));
                    // Đặt offset sao cho quân cờ luôn nằm giữa con trỏ chuột
                }
                /*    getValidMove()*/
            });
            board.appendChild(img);
        }
    }
}

// Lắng nghe di chuyển và thả chuột
board.addEventListener("mousemove", (e) => {
    if (!draggedPiece) return;
    const mousePos = getMousePositionInSvg(e);
    draggedPiece.setAttribute("x", mousePos.x - offsetCenter);
    draggedPiece.setAttribute("y", mousePos.y - offsetCenter);
});

board.addEventListener("mouseup", (e) => {
    if (!draggedPiece) return;
    const mousePos = getMousePositionInSvg(e);
    const { row, col } = getSquareAtCoordinates(mousePos.x, mousePos.y);
    const squareName = coordsToSquare(row, col); // Dùng hàm bạn đã có

    // Kiểm tra xem nước đi có hợp lệ không
    let validMoves = null;

    validMoves = getValidMoves(boardState, parseInt(draggedPiece.dataset.row), parseInt(draggedPiece.dataset.col));
    // Chuyển validMoves sang dạng string ["e4", "d3", ...]
    const validMoveSquares = validMoves.map(pos => coordsToSquare(pos.row, pos.col));
    console.log(validMoves);
    if (!validMoveSquares || !validMoveSquares.includes(squareName)) {
        returnToOriginalPosition(draggedPiece, startX, startY);
        clearDraggingState();
        return;
    }
    const { row: rowStart, col: colStart } = getSquareAtCoordinates(startX, startY);
    // Hợp lệ xóa quân cờ ở vị trí ăn 
    removePieceAt(row, col, draggedPiece);
    updateBoardState(boardState, rowStart, colStart, row, col);
    // Hợp lệ: đặt quân cờ
    draggedPiece.setAttribute("x", col * squareSize + offset);
    draggedPiece.setAttribute("y", row * squareSize + offset);
    draggedPiece.dataset.row = row;
    draggedPiece.dataset.col = col;
    clearDraggingState();
    console.log(boardState)
});

board.addEventListener("mouseleave", () => {
    if (draggedPiece) {
        returnToOriginalPosition(draggedPiece, startX, startY);
        board.classList.remove("dragging-active");
        draggedPiece.classList.remove("dragging");
        draggedPiece = null;
    }
});

function returnToOriginalPosition(piece, startX, startY) {
    piece.setAttribute("x", startX);
    piece.setAttribute("y", startY);
}
function getPieceNameById(e) {
    const clickedPieceId = e.target.id;
    console.log("Bạn đã click vào quân cờ có id:", clickedPieceId);
    return clickedPieceId;
}
function getMousePositionInSvg(e) {
    const pt = board.createSVGPoint();
    pt.x = e.clientX;
    pt.y = e.clientY;
    const svgP = pt.matrixTransform(board.getScreenCTM().inverse());
    return { x: svgP.x, y: svgP.y }

}

function getSquareAtCoordinates(x, y) {
    const col = Math.min(7, Math.max(0, Math.floor(x / squareSize)));
    const row = Math.min(7, Math.max(0, Math.floor(y / squareSize)));
    return { row, col };

}

//xóa trạng thái kéo thả 
function clearDraggingState() {
    if (draggedPiece) {
        draggedPiece.classList.remove("dragging");
        board.classList.remove("dragging-active");
        draggedPiece = null;
    }
}
// ăn quân 
function removePieceAt(row, col, draggedPiece = null) {
    const piece = document.querySelector(`.piece[data-row="${row}"][data-col="${col}"]`);
    {
        if (piece && piece !== draggedPiece) {
            piece.remove();
        }
    }
}
function removeAllHighlights() {
    const highlights = board.querySelectorAll(".highlight-origin, .highlight-destination");
    highlights.forEach(el => el.classList.remove("highlight-origin", "highlight-destination"));
}

function checkMove(fen, move) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Chess/Chess/MakeMove',
            type: 'POST',
            data: { fen: fen, move: move },
            success: function (response) {
                resolve(response);
            },
            error: function (xhr, status, error) {
                reject(error);
            }
        });
    });
}

function toChessNotation(row, col) {
    const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
    const ranks = ['8', '7', '6', '5', '4', '3', '2', '1'];
    return files[col] + ranks[row];
}

// Khởi tạo bàn cờ
drawBoard();
renderChess(boardState);