﻿using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System;

namespace ImpressaoEtiquetasControlCenter
{
    public class Impressao
    {
        public static void Imprimir(string Dados)
        {
            new Log(Dados);

            try
            {
                string NomeImpressoraPadrao = new ConfiguracaoDosParametros().DadosParametros.Where(x => x.Nome == "NOME IMPRESSORA DE ETIQUETAS").FirstOrDefault().Valor;

                List<string> Impressoras = PrinterSettings.InstalledPrinters.OfType<string>().ToList();

                string Impressora = string.Empty;

                foreach (var item in Impressoras)
                {                    
                    if (item.Contains(NomeImpressoraPadrao))
                    {
                        Impressora = item; 
                        break;
                    }
                }

                if (Impressora == string.Empty)
                {
                    new Log("Impressora não encontrada");
                    return;
                }

                PrintDocument pd = new PrintDocument();
                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;

                pd.PrinterSettings.PrinterName = Impressora;

                StringFormat AlinhadoEsquerda = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };
                StringFormat AlinhadoCentro = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                //string Dado = $"ImagemEtiqueta;{Id};{Descricao};{Peso};{CodBarra}";

                string Id = Dados.Split(';')[1];
                string Descricao = Dados.Split(';')[2];
                string Peso = Dados.Split(';')[3];
                string CodBarra = Dados.Split(';')[3] + "*" + Dados.Split(';')[4];

                Image CodBarras = GenCode128.Code128Rendering.MakeBarcodeImage(CodBarra, 2, true);

                /*Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0), 5);
                Rectangle rect = new Rectangle(11, 4, 215, 100);*/

                pd.PrintPage += (s, f) =>
                {
                    //f.Graphics.DrawRectangle(blackPen, rect);

                    if (Descricao.Length >= 25)
                    {
                        f.Graphics.DrawString(Descricao, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new Rectangle(11, 4, 215, 100), AlinhadoCentro);
                    }

                    else
                    {
                        f.Graphics.DrawString(Descricao, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Rectangle(11, 4, 215, 100), AlinhadoCentro);
                    }

                    f.Graphics.DrawString($"ID: {Id}", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Rectangle(11, 50, 215, 100), AlinhadoEsquerda);

                    f.Graphics.DrawString($"Peso: {Peso}", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Rectangle(125, 50, 215, 100), AlinhadoEsquerda);

                    f.Graphics.DrawImage(CodBarras, new Rectangle(11, 80, 210, 25));
                };

                pd.Print();
                pd.Dispose();

            }
            catch (Exception Ex)
            {
                using (new Log(Ex.ToString())) ;
            }
        }
      
    }
}
