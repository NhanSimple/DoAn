using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Engine
{
    public class StockfishEngine:IChessEngine
    {
        private Process _process;
        private StreamWriter _input;
        private StreamReader _output;
        private readonly string _enginePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Engines", "stockfish.exe");

        //public StockfishEngine(string enginePath)
        //{
        //    _enginePath = enginePath;
        //}

        public void Start()
        {
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
            SendCommand("isready");
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
                    if (parts.Length >= 1)
                    {
                        var move = parts[0].Trim();
                        if (move == moveUci)
                            return true;
                    }
                }
            }

            return false;
        }

        public async Task<string> GetBestMove(string fen, int depth = 15)
        {
            SendCommand($"position fen {fen}");
            SendCommand($"go depth {depth}");

            string line;
            while ((line = await ReadOutputAsync()) != null)
            {
                if (line.StartsWith("bestmove"))
                    return line.Split(' ')[1];
            }

            return null;
        }

        public void Stop()
        {
            _input?.Close();
            _process?.Kill();
            _process?.Dispose();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
