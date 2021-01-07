using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace CSVReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /*
     * Program uses NPOI package. Free of charge package to write Excel files without Excel. 
     * To get the latest code, please visit https://github.com/tonyqus/npoi.
    */
    public partial class MainWindow : Window
{
    string filePath;

    string newFileName;

    char separator =',';

    List<string> AllLines;

    bool WithHeader = false;


    public MainWindow()
    {
        InitializeComponent();
        Excel excel = new Excel();

    }

    private void openFile_Click(object sender, RoutedEventArgs e)
    {
        IWorkbook workbook = new XSSFWorkbook();
        ISheet worksheet = workbook.CreateSheet("Sheet1");
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.DefaultExt = ".csv";
        openFileDialog.Filter = "Dat documents(.csv)|*.csv";

        if (openFileDialog.ShowDialog() == true)
        {
            filePath = openFileDialog.FileName;
            AllLines = File.ReadAllLines(filePath).ToList();
        }

        else
        {
            MessageBox.Show("Incorrect file. Try again.");
        }

        int rownum = 0;

        foreach (string line in AllLines)
        {
            int cellnum = 0;

            var entries = line.Split(separator);   
            IRow row = worksheet.CreateRow(rownum);
            foreach(string entry in entries)
            {
                ICell cell = row.CreateCell(cellnum);
                cell.SetCellValue(entry);
                cellnum++;
            }

            rownum++;
        }

        var newFilePath = filePath.Replace(".csv", "");

        FileStream newWorkBook = File.Create($"{newFilePath}.xlsx");
        workbook.Write(newWorkBook);
        newWorkBook.Close();

    }
}
}
