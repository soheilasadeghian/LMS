using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Saham.ClassCollection
{
    public class Search
    {
        List<string> OR = new List<string>();
        List<string> AND = new List<string>();
        string searchString = "";

        static string[] stopWords ={ 
                        "‏دیگران"                        ,"همچنان"                        ,"مدت"                        ,"چیز"
                        ,"سایر"                        ,"جا"                        ,"طی"                        ,"کل"
                        ,"کنونی"                        ,"بیرون"                        ,"مثلا"                        ,"کامل"
                        ,"کاملا"                        ,"آنکه"                        ,"موارد"                        ,"واقعی"
                        ,"امور"                        ,"اکنون"                        ,"بطور"                        ,"بخشی"
                        ,"تحت"                        ,"چگونه"                        ,"عدم"                        ,"نوعی"
                        ,"حاضر"                        ,"وضع"                        ,"مقابل"                        ,"کنار"
                        ,"خویش"                        ,"نگاه"                        ,"درون"                        ,"زمانی"
                        ,"بنابراین"                        ,"تو"                        ,"خیلی"                        ,"بزرگ"
                        ,"خودش"                        ,"جز"                        ,"اینجا"                        ,"مختلف"
                        ,"توسط"                        ,"نوع"                        ,"همچنین"                        ,"آنجا"
                        ,"قبل"                        ,"جناح"                        ,"اینها"                        ,"طور"
                        ,"شاید"                        ,"ایشان"                        ,"جهت"                        ,"طریق"
                        ,"مانند"                        ,"پیدا"                        ,"ممکن"                        ,"کسانی"
                        ,"جای"                        ,"کسی"                        ,"غیر"                        ,"بی"
                        ,"قابل"                        ,"درباره"                        ,"جدید"                        ,"وقتی"
                        ,"اخیر"                        ,"چرا"                        ,"بیش"                        ,"روی"
                        ,"طرف"                        ,"جریان"                        ,"زیر"                        ,"آنچه"
                        ,"البته"                        ,"فقط"                        ,"چیزی"                        ,"چون"
                        ,"برابر"                        ,"هنوز"                        ,"بخش"                        ,"زمینه"
                        ,"بین"                        ,"بدون"                        ,"استفاد"                        ,"همان"
                        ,"نشان"                        ,"بسیاری"                        ,"بعد"                        ,"عمل"
                        ,"روز"                        ,"اعلام"                        ,"چند"                        ,"آنان"
                        ,"بلکه"                        ,"امروز"                        ,"تمام"                        ,"بیشتر"
                        ,"آیا"                        ,"برخی"                        ,"علیه"                        ,"دیگری"
                        ,"ویژه"                        ,"گذشته"                        ,"انجام"                        ,"حتی"
                        ,"داده"                        ,"راه"                        ,"سوی"                        ,"ولی"
                        ,"زمان"                        ,"حال"                        ,"تنها"                        ,"بسیار"
                        ,"یعنی"                        ,"عنوان"                        ,"همین"                        ,"هبچ"
                        ,"پیش"                        ,"وی"                        ,"یکی"                        ,"اینکه"
                        ,"وجود"                        ,"شما"                        ,"پس"                        ,"چنین"
                        ,"میان"                        ,"مورد"                        ,"چه"                        ,"اگر"
                        ,"همه"                        ,"نه"                        ,"دیگر"                        ,"آنها"
                        ,"باید"                        ,"هر"                        ,"او"                        ,"ما"
                        ,"من"                        ,"تا"                        ,"نیز"                        ,"اما"
                        ,"یک"                        ,"خود"                        ,"بر"                        ,"یا"
                        ,"هم"                        ,"را"                        ,"این"                        ,"با"
                        ,"آن"                        ,"برای"                        ,"و"                        ,"در"
                        ,"به"                        ,"که"                        ,"از"
                                   };

        public Search(string searchString)
        {
            this.searchString = searchString;

            var search = searchString.Split(' ').Where(c => c != " " && stopWords.Any(x => x == c) == false);
            var AND_word = search.Where(c => c.StartsWith("\"") && c.EndsWith("\""));
            search = search.Except(AND_word);
            AND_word = AND_word.Select(c => c.Replace("\"", ""));
            OR = search.ToList();
            AND = AND_word.ToList();
        }
        public List<string> getOR { get { return OR; } }
        public List<string> getAND { get { return AND; } }
    }
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}