namespace Shopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;

        public void OnGetContact()
        {
            this.Message = "Your email was sent.";
        }

        public void OnGetOrderSumbitted()
        { 
            this.Message = "Your order was submitted successfully. You will receive an email confirmation shortly.";
        }
    }
}
