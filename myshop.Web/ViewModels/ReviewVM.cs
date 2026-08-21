using System;

namespace myshop.Web.ViewModels;

public class ReviewVM
{
    public string UserId { get; set; }
    public string UserImg { get; set; }
    public string UserName { get; set; }
    public DateTime CreationDate { get; set; }
    public float Rate { get; set; }
    public string TheReview { get; set; }

}
