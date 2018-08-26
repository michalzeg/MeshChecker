using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IncoherentMeshChecker.ViewModel
{
    internal static class TableHeaderValidation
    {
        public static bool ValidateNodeTableHeaders(IDataObject dataInClipboard)
        {
            var clipboardOperations = new ClipboardOperations(dataInClipboard);

            var validation = new TableValidation();
            var firstRow = clipboardOperations.FirstRow;
            return validation.ValidateNodeHeader(firstRow);
        }

        public static bool ValidateElementTableHeaders(IDataObject dataInClipboard)
        {
            var clipboardOperations = new ClipboardOperations(dataInClipboard);

            var validation = new TableValidation();
            var firstRow = clipboardOperations.FirstRow;
            return validation.ValidateElementHeader(firstRow);
        }
    }
}