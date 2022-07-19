namespace Signup03.Models
{
    public class SearchView
    {
        public ActiveSearchParams? SearchParams { get; set; }
        public List<SignupForm>? SignupFormResult { get; set; }

        public SearchView()
        {
            SearchParams = new ActiveSearchParams();
            SignupFormResult = new List<SignupForm>();
        }

    }

    public class ActiveSearchParams
    {
        public string? SearchName { get; set; }
        public string? SearchMobile { get; set; }
        public  List<TblActiveItem>? tblActiveItems { get; set; }
    }
}
