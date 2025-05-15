const svg = document.querySelector(".chessboard");
const tileSize = 100;

for (let row = 0; row < 8; row++) {
    for (let col = 0; col < 8; col++) {
        const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");
/*        const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect")*/
        rect.setAttribute("x", col * tileSize);
        rect.setAttribute("y", row * tileSize);
        rect.setAttribute("width", tileSize);
        rect.setAttribute("height", tileSize);
        rect.setAttribute("fill", (row + col) % 2 === 0 ? "#f0d9b5" : "#b58863");
        svg.appendChild(rect);
    }
}

//// Setup vị trí quân cờ ban đầu
//const pieces = {
//    0: ['br', 'bn', 'bb', 'bq', 'bk', 'bb', 'bn', 'br'],
//    1: Array(8).fill('bp'),
//    6: Array(8).fill('wp'),
//    7: ['wr', 'wn', 'wb', 'wq', 'wk', 'wb', 'wn', 'wr']
//};

//for (const [row, pieceRow] of Object.entries(pieces)) {
//    pieceRow.forEach((piece, col) => {
//        const img = document.createElementNS(svgNS, "image");
//        img.setAttribute("href", `/Content/Images/${piece}.svg`);
//        img.setAttribute("x", col * squareSize);
//        img.setAttribute("y", row * squareSize);
//        img.setAttribute("width", squareSize);
//        img.setAttribute("height", squareSize);
//        img.setAttribute("class", "piece");
//        board.appendChild(img);
//    });
//}


//let draggedPiece = null;
//let offsetX = 0;
//let offsetY = 0;

//document.querySelectorAll('.piece').forEach(piece => {
//    piece.addEventListener('mousedown', (e) => {
//        draggedPiece = piece;
//        const rect = piece.getBoundingClientRect();
//        offsetX = e.clientX - rect.left;
//        offsetY = e.clientY - rect.top;

//        // Đưa quân cờ lên trên cùng bằng cách append lại
//        piece.parentNode.appendChild(piece);

//        const mouseMoveHandler = (e) => {
//            const svgRect = board.getBoundingClientRect();
//            const newX = e.clientX - svgRect.left - offsetX;
//            const newY = e.clientY - svgRect.top - offsetY;
//            draggedPiece.setAttribute('x', newX);
//            draggedPiece.setAttribute('y', newY);
//        };

//        const mouseUpHandler = () => {
//            document.removeEventListener('mousemove', mouseMoveHandler);
//            document.removeEventListener('mouseup', mouseUpHandler);

//            // TODO: Snap về ô gần nhất (bạn có thể thêm đoạn này sau)
//        };

//        document.addEventListener('mousemove', mouseMoveHandler);
//        document.addEventListener('mouseup', mouseUpHandler);
//    });
//});