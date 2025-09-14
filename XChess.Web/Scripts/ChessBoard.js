const board = document.querySelector(".chessboard");
const squareSize = 40;
let boardState = getInitialBoardState();

let startX = null;
let startY = null;
let draggedPiece = null;
let currentTurn = 'w';
const scale = 0.8;
const chessSize = squareSize * scale;
const offset = (squareSize - chessSize) / 2;
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
    const oldPieces = board.querySelectorAll(".piece");
    oldPieces.forEach(p => p.remove());

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
            img.dataset.row = row;
            img.dataset.col = col;

            img.addEventListener("mousedown", (e) => {
                const color = boardState[row][col][0];
                if (color !== currentTurn) return;

                draggedPiece = e.target;
                if (draggedPiece) {
                    draggedPiece.classList.add("dragging");
                    startX = parseFloat(draggedPiece.getAttribute("x"));
                    startY = parseFloat(draggedPiece.getAttribute("y"));
                    board.classList.add("dragging-active");
                }
            });
            board.appendChild(img);
        }
    }
}

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
    const squareName = coordsToSquare(row, col);

    const rowStart = parseInt(draggedPiece.dataset.row);
    const colStart = parseInt(draggedPiece.dataset.col);

    const validMoves = getValidMoves(boardState, rowStart, colStart);
    const validMoveSquares = validMoves.map(pos => coordsToSquare(pos.row, pos.col));

    if (!validMoveSquares.includes(squareName)) {
        returnToOriginalPosition(draggedPiece, startX, startY);
        clearDraggingState();
        return;
    }

    // Nếu đi phong tốt thì hiện dialog chọn quân phong
    const piece = boardState[rowStart][colStart];
    const promotionRank = piece[0] === 'w' ? 0 : 7;
    if (piece[1] === 'p' && row === promotionRank) {
        showPromotionDialog(row, col, rowStart, colStart);
    } else {
        makeMove(rowStart, colStart, row, col);
    }
});

board.addEventListener("mouseleave", () => {
    if (draggedPiece) {
        returnToOriginalPosition(draggedPiece, startX, startY);
        clearDraggingState();
    }
});

function showPromotionDialog(row, col, fromRow, fromCol) {
    const dialog = document.getElementById("promotion-dialog");
    const optionsContainer = dialog.querySelector(".promotion-options");
    const color = boardState[fromRow][fromCol][0];
    const piecePrefix = color === 'w' ? 'w' : 'b';

    optionsContainer.innerHTML = "";
    ['q', 'r', 'b', 'n'].forEach(p => {
        const img = document.createElement("img");
        img.src = `/Content/Images/${piecePrefix}${p}.svg`;
        img.dataset.piece = p;
        img.classList.add("promotion-choice");
        img.addEventListener("click", () => {
            makeMove(fromRow, fromCol, row, col, p);
            dialog.classList.add("hidden");
        });
        optionsContainer.appendChild(img);
    });

    dialog.classList.remove("hidden");
}

function makeMove(fromRow, fromCol, toRow, toCol, promotion = null) {
    updateBoardState(boardState, fromRow, fromCol, toRow, toCol, promotion);
    renderChess(boardState);
    clearDraggingState();

    currentTurn = currentTurn === 'w' ? 'b' : 'w';

    // Kiểm tra kết quả
    if (isKingInCheck(boardState, currentTurn)) {
        if (isCheckmate(boardState, currentTurn)) {
            showGameResult(`${currentTurn === 'w' ? "Trắng" : "Đen"} bị chiếu hết!`);
        } else {
            showGameResult(`${currentTurn === 'w' ? "Trắng" : "Đen"} đang bị chiếu!`);
        }
    }

    // Cập nhật trạng thái hiển thị (nếu có)
    // Ví dụ cập nhật status hoặc gọi AI đi
}

function returnToOriginalPosition(piece, startX, startY) {
    piece.setAttribute("x", startX);
    piece.setAttribute("y", startY);
}

function clearDraggingState() {
    if (draggedPiece) {
        draggedPiece.classList.remove("dragging");
        board.classList.remove("dragging-active");
        draggedPiece = null;
    }
}

function getMousePositionInSvg(e) {
    const pt = board.createSVGPoint();
    pt.x = e.clientX;
    pt.y = e.clientY;
    const svgP = pt.matrixTransform(board.getScreenCTM().inverse());
    return { x: svgP.x, y: svgP.y };
}

function getSquareAtCoordinates(x, y) {
    const col = Math.min(7, Math.max(0, Math.floor(x / squareSize)));
    const row = Math.min(7, Math.max(0, Math.floor(y / squareSize)));
    return { row, col };
}

function showGameResult(message) {
    document.getElementById("gameResultMessage").innerText = message;
    const modal = new bootstrap.Modal(document.getElementById('gameResultModal'));
    modal.show();
}

// Khởi tạo bàn cờ
drawBoard();
renderChess(boardState);
