using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImpressaoEtiquetasControlCenter
{

    public class Log :IDisposable
    {
        private bool disposedValue;

        public Log(string Logs)
        {
            try
            {
                string LogAnterior = string.Empty;
                string NovoLog = $"{DateTime.Now}: {Logs}{Environment.NewLine}";
              
                LogTxt(NovoLog);
            }
            catch
            {
               
            }
        }


        private void LogTxt(string Log)
        {
            if (!Directory.Exists(@"C:\LanSolutions\ServidorDeImpressao\Logs\"))
            {
                Directory.CreateDirectory(@"C:\LanSolutions\ServidorDeImpressao\Logs\");
            }

            string PathLog = @"C:\LanSolutions\ServidorDeImpressao\Logs\";

            string Data = DateTime.Now.ToString("dd" + "MM" + "yyyy");

            if (!Directory.Exists($@"{PathLog}{Data}\"))
            {
                Directory.CreateDirectory($@"{PathLog}{Data}\");
            }

            string PathLogHoje = PathLog + Data + @"\";

            if (!File.Exists(PathLogHoje + "Log.txt"))
            {
                File.Create(PathLogHoje + "Log.txt").Dispose();
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(PathLogHoje + "Log.txt", append: true))
                {
                    outputFile.Write(Log);
                }
            }
            catch
            {

            }



        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Log()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}