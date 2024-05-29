using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions
{
    public sealed class InvoiceNotFoundException : NotFoundException
    {
        #region Constructor
        public InvoiceNotFoundException(int invoiceId)
            : base($"The invoice with the identifier {invoiceId} was not found.")
        {

        }
        #endregion
    }
}
