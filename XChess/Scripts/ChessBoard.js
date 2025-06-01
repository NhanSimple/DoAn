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


function getInitialBoardState() {
    return [
        ['br', 'bn', 'bb', 'bq', 'bk', 'bb', 'bn', 'br'],
        ['bp', 'bp', 'bp', 'bp', 'bp', 'bp', 'bp', 'bp'],
        [null, null, null, null, null, null, null, null],
        [null, null, null, null, null, null, null, null],
        [null, null, null, null, null, null, null, null],
        [null, null, null, null, null, null, null, null],
        ['wp', 'wp', 'wp', 'wp', 'wp', 'wp', 'wp', 'wp'],
        ['wr', 'wn', 'wb', 'wq', 'wk', 'wb', 'wn', 'wr']
    ];
}

function coordsToSquare(row, col) {
    const file = String.fromCharCode('a'.charCodeAt(0) + col);
    const rank = 8 - row;
    return file + rank;
}


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
            img.setAttribute("x", col * squareSize);
            img.setAttribute("y", row * squareSize);
            img.setAttribute("width", squareSize);
            img.setAttribute("height", squareSize);
            img.setAttribute("class", "piece");
            img.setAttribute("id", pieceMap[piece]);
            img.dataset.row = row;
            img.dataset.col = col;

            // Sự kiện kéo bằng tay (drag thủ công)
            img.addEventListener("mousedown", (e) => {
                draggedPiece = e.target;
                startX = parseFloat(draggedPiece.getAttribute("x"));
                startY = parseFloat(draggedPiece.getAttribute("y"));
                // Đặt offset sao cho quân cờ luôn nằm giữa con trỏ chuột
                offsetX = squareSize / 2;
                offsetY = squareSize / 2;
            });

            board.appendChild(img);
        }
    }
}

// Lắng nghe di chuyển và thả chuột
board.addEventListener("mousemove", (e) => {
    if (!draggedPiece) return;
    const mousePos = getMousePositionInSvg(e);
    draggedPiece.setAttribute("x", mousePos.x - offsetX);
    draggedPiece.setAttribute("y", mousePos.y - offsetY);
});

board.addEventListener("mouseup", (e) => {
    if (!draggedPiece) return;
    const mousePos = getMousePositionInSvg(e);
    const { row, col } = getSquareAtCoordinates(mousePos.x, mousePos.y);

    // Đặt quân cờ vào đúng ô (căn giữa ô)
    draggedPiece.setAttribute("x", col * squareSize);
    draggedPiece.setAttribute("y", row * squareSize);
    draggedPiece.dataset.row = row;
    draggedPiece.dataset.col = col;
    draggedPiece = null;
});

board.addEventListener("mouseleave", () => {
    if (draggedPiece) {
        returnToOriginalPosition(draggedPiece, startX, startY);
        draggedPiece = null;
    }
});

board.addEventListener("click", (e) => {
    if (e.target.tagName === "image") {
        console.log("Click vào quân cờ ở ô", e.target.dataset.row, e.target.dataset.col);
        getPieceNameById(e);
    } else if (e.target.tagName === "rect") {
        const { x, y } = getMousePositionInSvg(e);
        const square = getSquareAtCoordinates(x, y);
        console.log(`Bạn đã click vào ô: ${square.row}, ${square.col}`);
    }
});

// quay lại vị trí gốc (cần chỉnh sửa lại)
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


//board.addEventListener("mousedown", startDragging);
//board.addEventListener("mousemove", dragging);
//board.addEventListener("mouseup", stopDragging);

// Khởi tạo bàn cờ
drawBoard();
renderChess(boardState);
///*Hello()*/