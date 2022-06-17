using System;
using System.IO;

namespace ImpressaoEtiquetasControlCenter
{
    public class BancoPostGres
    {
        public static string StringConexao = string.Empty;       

        private string LocalArquivoString = @"C:\LanSolutions\ServidorDeImpressao\ImpressaoEtiquetasControlCenter.ini";

        // LayoutString = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        public BancoPostGres()
        {
            string StringCryptografada = string.Empty;

            /*using (StreamWriter sw = File.CreateText(@"C:\LanSolutions\ControlCenter\ControlCenter.ini"))
            {
                sw.WriteLine(Cript.Encrypt("Server=10.40.100.90;Port=5432;Database=postgres;User Id=sulfrios;Password=Eus00o19;"));
            }*/

            try
            {
                if (File.Exists(LocalArquivoString))
                {
                    string Linha = File.ReadAllText(LocalArquivoString);             

                    if (string.IsNullOrEmpty(Linha))
                    {
                        throw new Exception("Arquivo de configuração não encontrado, sistema será fechado");
                    }

                    StringCryptografada = Linha;
                }

                else
                {
                    throw new Exception("Arquivo de configuração não encontrado, sistema será fechado");
                }

                StringConexao = Cript.Decrypt(StringCryptografada);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }
    }
}
