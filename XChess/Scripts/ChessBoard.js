const board = document.querySelector(".chessboard");
const tileSize = 40;
for (let row = 0; row < 8; row++) {
    for (let col = 0; col < 8; col++) {
        const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");
/*        const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect")*/
        rect.setAttribute("x", col * tileSize);
        rect.setAttribute("y", row * tileSize);
        rect.setAttribute("width", tileSize);
        rect.setAttribute("height", tileSize);
        rect.classList.add((row + col) % 2 === 0 ? "white-square" : "black-square");
        board.appendChild(rect);
    }
}

// Setup vị trí quân cờ ban đầu
const pieces = {
    0: ['br', 'bn', 'bb', 'bq', 'bk', 'bb', 'bn', 'br'],
    1: Array(8).fill('bp'),
    6: Array(8).fill('wp'),
    7: ['wr', 'wn', 'wb', 'wq', 'wk', 'wb', 'wn', 'wr']
};
for (const [row, pieceRow] of Object.entries(pieces)) 
    pieceRow.forEach((piece, col) => {
        const img = document.createElementNS("http://www.w3.org/2000/svg", "image");
        img.setAttribute("href", `/Content/Images/${piece}.svg`);
        img.setAttribute("x", col * tileSize);
        img.setAttribute("y", row * tileSize);
        img.setAttribute("width", tileSize);
        img.setAttribute("height", tileSize);
        img.setAttribute("class", "piece");
        board.appendChild(img);
    });


let selectedPiece = null;
let offsetX = 0;
let offsetY = 0;
let selectedTile = null;
let destinationTile = null;
let previousOriginTile = null;
board.addEventListener("mousedown", function (e) {
    if (e.target.tagName === "image" || e.target.tagName === "IMAGE") {
        selectedPiece = e.target;
        const rect = selectedPiece.getBoundingClientRect();
        offsetX = e.clientX - rect.x;
        offsetY = e.clientY - rect.y;
        board.appendChild(selectedPiece);

        // Xóa highlight cũ (nếu có)
        if (previousOriginTile) previousOriginTile.classList.remove("highlight-origin");
        if (destinationTile) destinationTile.classList.remove("highlight-destination");

        // Tìm ô hiện tại
        const x = parseInt(selectedPiece.getAttribute("x"));
        const y = parseInt(selectedPiece.getAttribute("y"));

        selectedTile = [...board.querySelectorAll("rect")].find(r =>
            parseInt(r.getAttribute("x")) === x &&
            parseInt(r.getAttribute("y")) === y
        );
        if (selectedTile) {
            selectedTile.classList.add("highlight-origin");
            previousOriginTile = selectedTile;
        }

    }
});


board.addEventListener("mousemove", function (e) {
    if (selectedPiece) {
        const pt = board.createSVGPoint();
        pt.x = e.clientX;
        pt.y = e.clientY;
        const svgP = pt.matrixTransform(board.getScreenCTM().inverse());
        selectedPiece.setAttribute("x", svgP.x - offsetX);
        selectedPiece.setAttribute("y", svgP.y - offsetY);
    }
});

board.addEventListener("mouseup", function (e) {
    if (selectedPiece) {
        const x = parseInt(selectedPiece.getAttribute("x"));
        const y = parseInt(selectedPiece.getAttribute("y"));

        const snappedX = Math.round(x / tileSize) * tileSize;
        const snappedY = Math.round(y / tileSize) * tileSize;

        selectedPiece.setAttribute("x", snappedX);
        selectedPiece.setAttribute("y", snappedY);

        // Highlight ô đích (vẫn giữ ô xuất phát)
        if (destinationTile) destinationTile.classList.remove("highlight-destination");

        destinationTile = [...board.querySelectorAll("rect")].find(r =>
            parseInt(r.getAttribute("x")) === snappedX &&
            parseInt(r.getAttribute("y")) === snappedY
        );
        if (destinationTile) destinationTile.classList.add("highlight-destination");

        selectedPiece = null;
    }
});