// Global mapping
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

function getValidMoves(boardState, row, col) {
    const piece = boardState[row][col];
    if (!piece) return [];

    const type = piece[1]; // ví dụ: 'p', 'r', 'n', ...

    switch (type) {
        case 'k':
            return getValidMovesKing(boardState, row, col);
        case 'p':
            return getValidMovesPawn(boardState, row, col);
        case 'r':
            return getValidMovesRook(boardState, row, col);
        case 'n':
            return getValidMovesKnight(boardState, row, col);
        case 'b':
            return getValidMovesBishop(boardState, row, col);
        case 'q':
            return getValidMovesQueen(boardState, row, col);
        default:
            return [];
    }
}
function getValidMovesPawn(boardState, row, col) {
    const moves = [];
    const piece = boardState[row][col];
    const color = piece[0]; // 'w' hoặc 'b'
    const direction = color === 'w' ? -1 : 1; // trắng đi lên (giảm row), đen đi xuống (tăng row)
    const startRow = color === 'w' ? 6 : 1;   // vị trí ban đầu của tốt

    // 1. Đi thẳng 1 ô nếu trống
    const forwardRow = row + direction;
    if (forwardRow >= 0 && forwardRow <= 7 && !boardState[forwardRow][col]) {
        moves.push({ row: forwardRow, col: col });

        // 2. Đi thẳng 2 ô nếu ở hàng xuất phát và 2 ô trống
        if (row === startRow && !boardState[forwardRow + direction][col]) {
            moves.push({ row: forwardRow + direction, col: col });
        }
    }

    // 3. Bắt chéo 2 bên nếu có quân địch
    for (let dc of [-1, 1]) {
        const c = col + dc;
        if (c < 0 || c > 7) continue;
        const r = forwardRow;
        if (r >= 0 && r <= 7) {
            const target = boardState[r][c];
            if (target && target[0] !== color) {
                moves.push({ row: r, col: c });
            }
        }
    }

    // 4. Bắt tốt qua đường (en passant)
    // enPassantTarget là {row, col} của ô mà tốt đối phương vừa nhảy qua
    //if (enPassantTarget) {
    //    if (Math.abs(enPassantTarget.col - col) === 1 && enPassantTarget.row === row) {
    //        // Vị trí tốt bên cạnh vừa nhảy 2 bước
    //        const captureRow = row + direction;
    //        if (captureRow >= 0 && captureRow <= 7) {
    //            moves.push({ row: captureRow, col: enPassantTarget.col });
    //        }
    //    }
    //}
    return moves;
}

// tượng - bishop
function getValidMovesBishop(boardState, row, col) {
    const moves = [];
    const piece = boardState[row][col];
    const color = piece[0];
    const directions = [
        [-1, 1],  // ↗️
        [1, 1],   // ↘️
        [1, -1],  // ↙️
        [-1, -1]  // ↖️
    ];

    for (const [dx, dy] of directions) {
        let r = row + dx;
        let c = col + dy;
        while (r >= 0 && r <= 7 && c >= 0 && c <= 7) {
            const target = boardState[r][c];
            if (target === null) {
                moves.push({ row: r, col: c });
            } else {
                if (target[0] !== color) {
                    moves.push({ row: r, col: c });
                }
                break;
            }
            r += dx;
            c += dy;
        }
    }
    return moves;
}

//mã - kninght
function getValidMovesKnight(boardState, row, col) {
    const moves = [];
    const piece = boardState[row][col];
    const color = piece[0];
    const directions = [
        [-2, -1], [-2, 1],
        [-1, -2], [-1, 2],
        [2, -1], [2, 1],
        [1, -2], [1, 2]
    ];
    for (const [dx, dy] of directions) {
        const r = row + dx;
        const c = col + dy;
        if (r >= 0 && r <= 7 && c >= 0 && c <= 7) {
            const target = boardState[r][c];
            if (!target || target[0] !== color) {
                moves.push({ row: r, col: c });
            }
        }
    }
    return moves;
}

//xe - rook 
function getValidMovesRook(boardState, row, col) {
    const moves = [];
    const piece = boardState[row][col];
    const color = piece[0];
    const directions = [
        [-1, 0],
        [1, 0],
        [0, -1],
        [0, 1],
    ];
    for (const [dx, dy] of directions) {
        let r = row + dx;
        let c = col + dy;
        while (r >= 0 && r <= 7 && c >= 0 && c <= 7) {
            const target = boardState[r][c];
            if (!target) {
                moves.push({ row: r, col: c });
            } else {
                if (target[0] !== color) {
                    moves.push({ row: r, col: c })
                };
                break;
            }
            r += dx;
            c += dy;
        }
    }

    return moves;
}

//vua - king
function getValidMovesKing(boardState, row, col) {
    const moves = [];
    const piece = boardState[row][col];
    const color = piece[0];
    const directions = [
        [-1, 0], [1, 0],
        [0, -1], [0, 1],
        [-1, 1], [1, 1],
        [1, -1], [-1, -1]
    ];
    for (const [dx, dy] of directions) {
        const r = row + dx;
        const c = col + dy;
        if (r >= 0 && r <= 7 && c >= 0 && c <= 7) {
            const target = boardState[r][c];
            if (!target || target[0] !== color) {
                moves.push({ row: r, col: c });
            }
        }
    }

    return moves;
}

//hậu - queen
function getValidMovesQueen(boardState, row, col) {
    const moves = [];
    const piece = boardState[row][col];
    if (!piece) return moves;
    const color = piece[0];
    const directions = [
        [-1, 0], [1, 0],
        [0, -1], [0, 1],
        [-1, 1], [1, 1],
        [1, -1], [-1, -1]
    ];
    for (const [dx, dy] of directions) {
        let r = row + dx;
        let c = col + dy;
        while (r >= 0 && r <= 7 && c >= 0 && c <= 7) {
            const target = boardState[r][c];
            if (!target) {
                moves.push({ row: r, col: c });
            } else {
                if (target[0] !== color) {
                    moves.push({ row: r, col: c });
                }
                break;
            }
            r += dx;
            c += dy;
        }
    }

    return moves;
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

// cập nhật boarState sau mỗi lần di chuyển 
function updateBoardState(boardState, fromRow, fromCol, toRow, toCol) {
    boardState[toRow][toCol] = boardState[fromRow][fromCol];
    boardState[fromRow][fromCol] = null;
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