using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace XChess.Infrastructure.StockfishEngine
{
    public class StockfishEngine : IStockfishEngine
    {
        private Process _process;
        private StreamWriter _input;
        private StreamReader _output;
        private readonly string _enginePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Engines", "stockfish.exe");

        public void Start()
        {
            if (!File.Exists(_enginePath))
                throw new FileNotFoundException("Không tìm thấy Stockfish tại: " + _enginePath);

            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _enginePath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            _process.Start();
            _input = _process.StandardInput;
            _output = _process.StandardOutput;

            SendCommand("uci");

            // Đợi xác nhận "uciok"
            string line;
            while ((line = _output.ReadLine()) != null)
            {
                if (line.Trim() == "uciok") break;
            }

            SendCommand("isready");
            while ((line = _output.ReadLine()) != null)
            {
                if (line.Trim() == "readyok") break;
            }
        }
        public void SendCommand(string command)
        {
            _input.WriteLine(command);
            _input.Flush();
        }

        public async Task<string> ReadOutputAsync()
        {
            return await _output.ReadLineAsync();
        }

        public async Task<bool> IsMoveLegal(string fen, string moveUci)
        {
            SendCommand($"position fen {fen}");
            SendCommand("go perft 1");

            string line;
            while ((line = await ReadOutputAsync()) != null)
            {
                if (line.StartsWith("Nodes searched:"))
                    break;

                if (line.Contains(":"))
                {
                    var parts = line.Split(':');
                    var move = parts[0].Trim();
                    if (move == moveUci)
                        return true;
                }
            }

            return false;
        }

        public async Task<string> GetBestMove(string fen, int depth)
        {
            SendCommand($"position fen {fen}");
            if (depth > 0)
                SendCommand($"go depth {depth}");
            else
                SendCommand("go movetime 1000"); // nếu depth = -1 thì cho máy chạy 1s

            string line;
            while ((line = await ReadOutputAsync()) != null)
            {
                if (line.StartsWith("bestmove"))
                    return line.Split(' ')[1];
            }

            return null;
        }

        public async Task<bool> ApplyMoveAsync(string fen, string moveUci)
        {
            SendCommand($"position fen {fen} moves {moveUci}");
            SendCommand("go perft 1");

            string line;
            bool moveIsLegal = false;

            while ((line = await ReadOutputAsync()) != null)
            {
                if (line.StartsWith("Nodes searched:"))
                    break;

                if (line.Contains(":"))
                {
                    var parts = line.Split(':');
                    var move = parts[0].Trim();
                    if (move == moveUci)
                    {
                        moveIsLegal = true;
                        break;
                    }
                }
            }

            return moveIsLegal;
        }

        public void Stop()
        {
            try
            {
                _input?.Close();
                if (!_process.HasExited)
                {
                    _process.Kill();
                }
                _process?.Dispose();
            }
            catch { /* ignore exceptions on dispose */ }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
