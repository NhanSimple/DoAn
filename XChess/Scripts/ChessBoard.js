const board = document.getElementById('chessboard');

// Các quân cờ ban đầu
const initialBoard = [
    ['r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'], // Quân cờ đen
    ['p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'], // Tốt đen
    ['', '', '', '', '', '', '', ''], // Dòng trống giữa
    ['', '', '', '', '', '', '', ''],
    ['', '', '', '', '', '', '', ''],
    ['', '', '', '', '', '', '', ''],
    ['P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'], // Tốt trắng
    ['R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R']  // Quân cờ trắng
];

// Mã Unicode của quân cờ
const pieces = {
    'k': '♚', 'q': '♛', 'r': '♜', 'b': '♝', 'n': '♞', 'p': '♟', // QUÂN ĐEN
    'K': '♔', 'Q': '♕', 'R': '♖', 'B': '♗', 'N': '♘', 'P': '♙'  // QUÂN TRẮNG
};

// Hàm tạo bàn cờ

function createBoard() {
    for (let row = 0; row < 8; row++) {
        for (let col = 0; col < 8; col++) {
            const square = document.createElement('div');
            square.classList.add('square');
            if ((row + col) % 2 === 0) {
                square.classList.add('white');
            } else {
                square.classList.add('black');
            }

            const piece = initialBoard[row][col];
            if (piece) {
                const pieceElement = document.createElement('span');
                pieceElement.classList.add('piece');
                pieceElement.textContent = pieces[piece];
                pieceElement.setAttribute('draggable', 'true');
                square.appendChild(pieceElement);
            }

            board.appendChild(square);
        }
    }
}

// Gọi hàm tạo bàn cờ trước
createBoard();

// ✅ Gán sự kiện sau khi bàn cờ đã được render
let draggedPiece = null;

// Bắt đầu kéo
document.querySelectorAll('.piece').forEach(piece => {
    piece.addEventListener('dragstart', (e) => {
        draggedPiece = piece;
        setTimeout(() => {
            piece.style.display = 'none';
        }, 0);
    });

    piece.addEventListener('dragend', (e) => {
        piece.style.display = '';
    });
});

// Cho phép drop vào các ô
document.querySelectorAll('.square').forEach(square => {
    square.addEventListener('dragover', (e) => {
        e.preventDefault(); // Bắt buộc phải có
    });

    square.addEventListener('drop', (e) => {
        e.preventDefault();
        if (draggedPiece) {
            square.appendChild(draggedPiece);
        }
    });
});