// Global mapping


// Đảo ngược pieceMap để dùng cho FEN → boardState

const pieceMap = {
    'br': 'r', 'bn': 'n', 'bb': 'b', 'bq': 'q', 'bk': 'k', 'bp': 'p',
    'wr': 'R', 'wn': 'N', 'wb': 'B', 'wq': 'Q', 'wk': 'K', 'wp': 'P'
};

const reversePieceMap = Object.fromEntries(
    Object.entries(pieceMap).map(([k, v]) => [v, k])
);
function getLogicalCoords(row, col, flipped) {
    return {
        row: flipped ? 7 - row : row,
        col: flipped ? 7 - col : col
    };
}

function boardStateToFEN(boardState, turn) {
    let fen = '';
    for (let row of boardState) {
        let emptyCount = 0;
        for (let cell of row) {
            if (cell === null) {
                emptyCount++;
            } else {
                if (emptyCount > 0) {
                    fen += emptyCount;
                    emptyCount = 0;
                }
                fen += pieceMap[cell];
            }
        }
        if (emptyCount > 0) {
            fen += emptyCount;
        }
        fen += '/';
    }

    fen = fen.slice(0, -1);
    fen += ` ${turn % 2 == 0 ? 'w' : 'b'} KQkq - 0 1`;
    return fen;
}
function fenToBoardState(fen) {
    const boardState = [];
    const rows = fen.split(' ')[0].split('/');
    for (let row of rows) {
        const boardRow = [];
        for (let char of row) {
            if (isNaN(char)) {
                boardRow.push(reversePieceMap[char]);
            } else {
                for (let i = 0; i < parseInt(char); i++) {
                    boardRow.push(null);
                }
            }
        }
    }
}


//function getPawnMoves(row, col, board) {
//    let moves = [];
//    moves = moves.concat(forwardOne(row, col, board));
//    moves = moves.concat(forwardTwo(row, col, board));
//    moves = moves.concat(captureDiagonals(row, col, board));
//    moves = moves.concat(enPassant(row, col, board));
//    moves = moves.concat(promotionMoves(row, col, board));
//    return moves;
//}

//function forwardOne(row, col, board) {
//    const moves = [];
//    const piece = board[row][col];
//    const dir = piece.color === 'white' ? -1 : 1; // trắng đi lên, đen đi xuống
//    const nextRow = row + dir;

//    if (nextRow >= 0 && nextRow < 8 && board[nextRow][col] === null) {
//        moves.push({ from: [row, col], to: [nextRow, col] });
//    }
//    return moves;
//}

//function forwardTwo(row, col, board) {
//    const moves = [];
//    const piece = board[row][col];
//    const dir = piece.color === 'white' ? -1 : 1;
//    const startRow = piece.color === 'white' ? 6 : 1;
//    if (row === startRow && board[row + dir][col] === null && board[row + 2 * dir][col] === null) {
//        moves.push({ from: [row, col], to: [row + 2 * dir, col] });
//    }
//    return moves;
//}

//function captureDiagonals(row, col, board) {
//    const moves = [];
//    const piece = board[row][col];
//    const dir = piece.color === 'white' ? -1 : 1;
//    const nextRow = row + dir;
//    const possibleCols = [col - 1, col + 1];

//    possibleCols.forEach(c => {
//        if (c >= 0 && c < 8) {
//            const target = board[nextRow][c];
//            if (target && target.color !== piece.color) {
//                moves.push({ from: [row, col], to: [nextRow, c] });
//            }
//        }
//    });
//    return moves;
//}

//function enPassant(row, col, board) {
//    // Cần dữ liệu trạng thái ván cờ để kiểm tra en passant, ví dụ vị trí quân vừa đi 2 ô
//    return [];
//}

//function promotionMoves(row, col, board) {
//    // Cần thêm logic cho phong cấp
//    return [];
//}
//function isInBounds(row, col) {
//    return row >= 0 && row < 8 && col >= 0 && col < 8;
//}