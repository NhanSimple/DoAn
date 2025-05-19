const board = document.querySelector(".chessboard");
const tileSize = 40;
let boardState = getInitialBoardState();

let selectedPiece = null;
let offsetX = 0;
let offsetY = 0;
let previousOriginTile = null;
let destinationTile = null;

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

function drawBoard() {
    for (let row = 0; row < 8; row++) {
        for (let col = 0; col < 8; col++) {
            const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");
            rect.setAttribute("x", col * tileSize);
            rect.setAttribute("y", row * tileSize);
            rect.setAttribute("width", tileSize);
            rect.setAttribute("height", tileSize);
            rect.classList.add((row + col) % 2 === 0 ? "white-square" : "black-square");
            board.appendChild(rect);
        }
    }
}

function renderBoard(state) {
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
            img.setAttribute("x", col * tileSize);
            img.setAttribute("y", row * tileSize);
            img.setAttribute("width", tileSize);
            img.setAttribute("height", tileSize);
            img.setAttribute("class", "piece");
            img.dataset.row = row;
            img.dataset.col = col;
            board.appendChild(img);
        }
    }
}

function getBoardCoordinatesFromMouseEvent(e) {
    const pt = board.createSVGPoint();
    pt.x = e.clientX;
    pt.y = e.clientY;
    const svgP = pt.matrixTransform(board.getScreenCTM().inverse());
    return { x: svgP.x, y: svgP.y };
}

function getTileAtCoordinates(x, y) {
    const col = Math.min(7, Math.max(0, Math.floor(x / tileSize)));
    const row = Math.min(7, Math.max(0, Math.floor(y / tileSize)));
    return { row, col };
}

function highlightTile(row, col, className) {
    const rects = board.querySelectorAll("rect");
    rects.forEach(r => r.classList.remove(className));

    const rect = [...rects].find(r =>
        parseInt(r.getAttribute("x")) === col * tileSize &&
        parseInt(r.getAttribute("y")) === row * tileSize
    );
    if (rect) {
        rect.classList.add(className);
        return rect;
    }
    return null;
}

function startDragging(e) {
    if (!e.target.classList.contains("piece")) return;

    selectedPiece = e.target;
    const rect = selectedPiece.getBoundingClientRect();
    offsetX = e.clientX - rect.x;
    offsetY = e.clientY - rect.y;

    // Đưa quân cờ lên trên cùng
    board.appendChild(selectedPiece);

    // Highlight ô xuất phát
    const row = parseInt(selectedPiece.dataset.row);
    const col = parseInt(selectedPiece.dataset.col);

    if (previousOriginTile) previousOriginTile.classList.remove("highlight-origin");
    previousOriginTile = highlightTile(row, col, "highlight-origin");

    if (destinationTile) {
        destinationTile.classList.remove("highlight-destination");
        destinationTile = null;
    }
}

function dragging(e) {
    if (!selectedPiece) return;

    const { x, y } = getBoardCoordinatesFromMouseEvent(e);
    selectedPiece.setAttribute("x", x - offsetX);
    selectedPiece.setAttribute("y", y - offsetY);
}

function stopDragging(e) {
    if (!selectedPiece) return;

    const { x, y } = getBoardCoordinatesFromMouseEvent(e);
    const { row: newRow, col: newCol } = getTileAtCoordinates(x, y);

    const snappedX = newCol * tileSize;
    const snappedY = newRow * tileSize;

    const oldRow = parseInt(selectedPiece.dataset.row);
    const oldCol = parseInt(selectedPiece.dataset.col);

    // Nếu không di chuyển thì trả về vị trí cũ
    if (newRow === oldRow && newCol === oldCol) {
        selectedPiece.setAttribute("x", oldCol * tileSize);
        selectedPiece.setAttribute("y", oldRow * tileSize);
    } else {
        // Cập nhật trạng thái bàn cờ
        boardState[newRow][newCol] = boardState[oldRow][oldCol];
        boardState[oldRow][oldCol] = null;

        // Render lại bàn cờ với trạng thái mới
        renderBoard(boardState);

        // Highlight ô đích
        if (destinationTile) destinationTile.classList.remove("highlight-destination");
        destinationTile = highlightTile(newRow, newCol, "highlight-destination");
    }

    selectedPiece = null;
}

board.addEventListener("mousedown", startDragging);
board.addEventListener("mousemove", dragging);
board.addEventListener("mouseup", stopDragging);

// Khởi tạo bàn cờ
drawBoard();
renderBoard(boardState);