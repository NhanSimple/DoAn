const board = document.querySelector(".chessboard");
const tileSize = 40;
let boardState = getInitialBoardState();
let selectedPiece = null;
let offsetX = 0;
let offsetY = 0;
let previousOriginTile = null;
let destinationTile = null;
let fen = boardStateToFEN(getInitialBoardState(), 0);
let move = null;
let turn = 0;

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
        // đổi lượt


        boardState[newRow][newCol] = boardState[oldRow][oldCol];
        boardState[oldRow][oldCol] = null;
        move = toChessNotation(oldRow, oldCol) + toChessNotation(newRow, newCol);
        console.log(turn)
        console.log(fen)
        console.log(move)

        checkMove(fen, move)
        turn++;
        fen = boardStateToFEN(boardState, turn);
        console.log(fen)
        // Render lại bàn cờ với trạng thái mới
        renderBoard(boardState);


        // Highlight ô đích
        if (destinationTile) destinationTile.classList.remove("highlight-destination");
        destinationTile = highlightTile(newRow, newCol, "highlight-destination");
    }

    selectedPiece = null;
}


function boardStateToFEN(boardState, turn) {
    const pieceMap = {
        'br': 'r', 'bn': 'n', 'bb': 'b', 'bq': 'q', 'bk': 'k', 'bp': 'p',
        'wr': 'R', 'wn': 'N', 'wb': 'B', 'wq': 'Q', 'wk': 'K', 'wp': 'P'
    };

    let fen = '';

    for (let row of boardState) {
        let emptyCount = 0;
        for (let cell of row) {
            if (cell === null) {
                emptyCount++
            } else {
                if (emptyCount > 0) {
                    fen += emptyCount
                    emptyCount = 0
                }
                fen += pieceMap[cell]
            }
        }
        if (emptyCount > 0) {
            fen += emptyCount;
        }
        fen += '/'
    }

    fen = fen.slice(0, -1); // Xoá dấu '/' cuối cùng

    // Thêm các thông tin còn lại để FEN hợp lệ (mặc định ban đầu):
    // "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
    fen += ` ${turn % 2 == 0 ? 'w' : 'b'} KQkq - 0 1`;
    return fen;
}


function checkMove(fen, move) {
    $.ajax({
        url: '/Chess/Chess/MakeMove',
        type: 'POST',
        data: {
            fen: fen, // ví dụ: FEN bàn cờ lúc đầu
            move: move // ví dụ nước đi
        },
        success: function (response) {
            alert("kết quả kiểm tra: " + response.legal);
        },
        error: function (xhr, status, error) {
            alert("Lỗi: " + error);
        }
    });
}

function toChessNotation(row, col) {
    const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
    const ranks = ['8', '7', '6', '5', '4', '3', '2', '1'];
    return files[col] + ranks[row];
}


board.addEventListener("mousedown", startDragging);
board.addEventListener("mousemove", dragging);
board.addEventListener("mouseup", stopDragging);

// Khởi tạo bàn cờ
drawBoard();
renderBoard(boardState);