const board = document.querySelector(".chessboard");
const squareSize = 40;
let boardState = getInitialBoardState();
let offsetX = 0
let offsetY = 0;
let selectedPiece = null;
let previousSquare = null;
let destinationSquare = null;
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
                const mousePos = getMousePositionInSvg(e);
                const pieceX = parseFloat(draggedPiece.getAttribute("x"));
                const pieceY = parseFloat(draggedPiece.getAttribute("y"));
                offsetX = mousePos.x - pieceX;
                offsetY = mousePos.y - pieceY;
            });

            board.appendChild(img);
        }
    }
}

// Lắng nghe di chuyển và thả chuột
board.addEventListener("mousemove", (e) => {
    if (!draggedPiece) return;
    const { x, y } = getMousePositionInSvg(e);
    draggedPiece.setAttribute("x", x - offsetX);
    draggedPiece.setAttribute("y", y - offsetY);
});

board.addEventListener("mouseup", (e) => {
    if (!draggedPiece) return;
    const { x, y } = getMousePositionInSvg(e);
    const { row, col } = getSquareAtCoordinates(x, y);

    // Cập nhật tọa độ mới cho quân cờ (làm tròn về ô)
    draggedPiece.setAttribute("x", col * squareSize);
    draggedPiece.setAttribute("y", row * squareSize);
    draggedPiece.dataset.row = row;
    draggedPiece.dataset.col = col;

    draggedPiece = null;
});

board.addEventListener("mouseleave", () => {
    draggedPiece = null;
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
    const row = Math.min(7, 7 - Math.max(0, Math.floor(y / squareSize)));
    return { row, col };

}
function removeAllHighlights() {
    const highlights = board.querySelectorAll(".highlight-origin, .highlight-destination");
    highlights.forEach(el => el.classList.remove("highlight-origin", "highlight-destination"));
}
//function selectSquare(rect, boardState) {
//    //cần chỉnh lại currentTurn 
//    if (currentTurn=="w") {

//        //const row = parseInt(rect.dataset.row);
//        //const col = parseInt(rect.dataset.col);
//        //const piece = boardState[row][col];
//        //const squareName = rect.dataset.square;
// /*       console.log("Click vào:", squareName, piece);*/
//        if (destinationSquare) {
//            removeAllHighlights();
//        }
//        else {
//            if (previousSquare) {
//                rect.classList.add("highlight-destination")
//                destinationSquare = rect;
//            } else {
//                rect.classList.add("highlight-origin");
//                previousSquare = rect;
//            }
//        }
//    }
//}


//function getLogicalCoords(row, col, flipped) {
//    return {
//        row: flipped ? 7 - row : row,
//        col: flipped ? 7 - col : col
//    };
//}

//document.querySelectorAll('.piece').forEach(piece => {
//    piece.addEventListener('dragstart', e => {
//        const parent = piece.parentElement;
//        const row = parseInt(parent.dataset.row);
//        const col = parseInt(parent.dataset.col);
//        const { row: logicalRow, col: logicalCol } = getLogicalCoords(row, col, flipped);

//        // Truyền theo tọa độ logic
//        e.dataTransfer.setData("text/plain", `${logicalRow},${logicalCol}`);
//    });
//});


//document.querySelectorAll('.square').forEach(square => {
//    square.addEventListener('dragover', e => {
//        e.preventDefault(); // cho phép drop
//        square.classList.add('highlight'); // hiệu ứng hover
//    });

//    square.addEventListener('dragleave', e => {
//        square.classList.remove('highlight');
//    });
//});


//document.querySelectorAll('.square').forEach(square => {
//    square.addEventListener('drop', e => {
//        e.preventDefault();
//        square.classList.remove('highlight');

//        const [fromRow, fromCol] = e.dataTransfer.getData("text/plain").split(',').map(Number);

//        const toRow = parseInt(square.dataset.row);
//        const toCol = parseInt(square.dataset.col);
//        const { row: logicalToRow, col: logicalToCol } = getLogicalCoords(toRow, toCol, flipped);

//        // Giờ bạn có fromRow, fromCol, logicalToRow, logicalToCol → xử lý nước đi
//        console.log(`Di chuyển từ (${fromRow}, ${fromCol}) đến (${logicalToRow}, ${logicalToCol})`);

//        // Gọi hàm movePiece(fromRow, fromCol, logicalToRow, logicalToCol)...
//    });
//});
//function getBoardCoordinatesFromMouseEvent(e) {
//    const pt = board.createSVGPoint();
//    pt.x = e.clientX;
//    pt.y = e.clientY;
//    const svgP = pt.matrixTransform(board.getScreenCTM().inverse());
//    return { x: svgP.x, y: svgP.y };
//}

//function getTileAtCoordinates(x, y) {
//    const col = Math.min(7, Math.max(0, Math.floor(x / squareSize)));
//    const row = Math.min(7, Math.max(0, Math.floor(y / squareSize)));
//    return { row, col };
//}

//function highlightTile(row, col, className) {
//    const rects = board.querySelectorAll("rect");
//    rects.forEach(r => r.classList.remove(className));

//    const rect = [...rects].find(r =>
//        parseInt(r.getAttribute("x")) === col * squareSize &&
//        parseInt(r.getAttribute("y")) === row * squareSize
//    );
//    if (rect) {
//        rect.classList.add(className);
//        return rect;
//    }
//    return null;
//}

//function startDragging(e) {
//    if (!e.target.classList.contains("piece")) return;

//    selectedPiece = e.target;
//    const rect = selectedPiece.getBoundingClientRect();
//    offsetX = squareSize / 2;
//    offsetY = squareSize / 2;
//    const { x, y } = getBoardCoordinatesFromMouseEvent(e);
//    selectedPiece.setAttribute("x", x - offsetX);
//    selectedPiece.setAttribute("y", y - offsetY);
//    // Đưa quân cờ lên trên cùng
//    board.appendChild(selectedPiece);

//    // Highlight ô xuất phát
//    const row = parseInt(selectedPiece.dataset.row);
//    const col = parseInt(selectedPiece.dataset.col);

//    if (previousOriginTile) previousOriginTile.classList.remove("highlight-origin");
//    previousOriginTile = highlightTile(row, col, "highlight-origin");

//    if (destinationTile) {
//        destinationTile.classList.remove("highlight-destination");
//        destinationTile = null;
//    }
//}

//function dragging(e) {
//    if (!selectedPiece) return;

//    const { x, y } = getBoardCoordinatesFromMouseEvent(e);
//    selectedPiece.setAttribute("x", x - offsetX);
//    selectedPiece.setAttribute("y", y - offsetY);
//}

//async function stopDragging(e) {
//    if (!selectedPiece) return;

//    const svgP = getBoardCoordinatesFromMouseEvent(e);
//    const { row: newRow, col: newCol } = getTileAtCoordinates(svgP.x, svgP.y);

//    const oldRow = parseInt(selectedPiece.dataset.row);
//    const oldCol = parseInt(selectedPiece.dataset.col);

//    if (newRow === oldRow && newCol === oldCol) {
//        selectedPiece.setAttribute("x", oldCol * squareSize);
//        selectedPiece.setAttribute("y", oldRow * squareSize);
//        selectedPiece = null;
//        return;
//    }

//    const capturedPiece = boardState[newRow][newCol];
//    let capturedElement = null;

//    if (capturedPiece) {
//        capturedElement = [...board.querySelectorAll(".piece")].find(p =>
//            parseInt(p.dataset.row) === newRow && parseInt(p.dataset.col) === newCol
//        );
//        if (capturedElement) capturedElement.remove();
//    }

//    boardState[newRow][newCol] = boardState[oldRow][oldCol];
//    boardState[oldRow][oldCol] = null;

//    selectedPiece.setAttribute("x", newCol * squareSize);
//    selectedPiece.setAttribute("y", newRow * squareSize);

//    move = toChessNotation(oldRow, oldCol) + toChessNotation(newRow, newCol);

//    try {
//        const response = await checkMove(fen, move);
//        if (response.legal) {
//            selectedPiece.dataset.row = newRow;
//            selectedPiece.dataset.col = newCol;

//            turn++;
//            fen = boardStateToFEN(boardState, turn);

//            if (destinationTile) destinationTile.classList.remove("highlight-destination");
//            destinationTile = highlightTile(newRow, newCol, "highlight-destination");
//        } else {
//            // Nước đi không hợp lệ, phục hồi trạng thái
//            selectedPiece.setAttribute("x", oldCol * squareSize);
//            selectedPiece.setAttribute("y", oldRow * squareSize);

//            boardState[oldRow][oldCol] = boardState[newRow][newCol];
//            boardState[newRow][newCol] = capturedPiece;

//            if (capturedElement) board.appendChild(capturedElement);
//        }
//    } catch (error) {
//        alert("Lỗi kiểm tra nước đi.");
//        selectedPiece.setAttribute("x", oldCol * squareSize);
//        selectedPiece.setAttribute("y", oldRow * squareSize);

//        boardState[oldRow][oldCol] = boardState[newRow][newCol];
//        boardState[newRow][newCol] = capturedPiece;

//        if (capturedElement) board.appendChild(capturedElement);
//    } finally {
//        selectedPiece = null;
//    }
//}




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