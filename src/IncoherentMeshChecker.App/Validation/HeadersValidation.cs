using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IncoherentMeshChecker.ViewModel
{
    static class HeaderTablesValidation
    {
        public static bool ValidateNodeTableHeaders(IDataObject dataInClipboard)
        {
            ClipboardOperations clipboardOperations = new ClipboardOperations(dataInClipboard);
            //var headers = clipboardOperations.GetItemsInFirstRow();
            TableValidation validation = new TableValidation();
            var firstRow = clipboardOperations.FirstRow;
            return validation.ValidateNodeHeader(firstRow); 

        }
        public static bool ValidateElementTableHeaders(IDataObject dataInClipboard)
        {
            ClipboardOperations clipboardOperations = new ClipboardOperations(dataInClipboard);
            //var headers = clipboardOperations.GetItemsInFirstRow();
            TableValidation validation = new TableValidation();
            var firstRow = clipboardOperations.FirstRow;
            return validation.ValidateElementHeader(firstRow);
        }
    }
}
