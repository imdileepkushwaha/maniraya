using DataTier;
using System;
using System.Data;

public static class FaqHelper
{
    static readonly Tuple<string, string>[] DefaultFaqs = new[]
    {
        Tuple.Create(
            "How do I place an order on Maniraya?",
            "Browse products on the home page, open a product, add it to cart, and complete checkout with your delivery details and payment method."),
        Tuple.Create(
            "What payment methods are accepted?",
            "We accept online payment options shown at checkout, including UPI, cards, and net banking. Available methods may vary by order."),
        Tuple.Create(
            "How can I track my order?",
            "After placing an order, use Track Order from the website header or login to your account to view order status and updates."),
        Tuple.Create(
            "What is the return and refund policy?",
            "Eligible products can be returned within the return window mentioned on the product page. Refunds are processed after the returned item is verified."),
        Tuple.Create(
            "How do I register as a Maniraya member?",
            "Click Sign Up on the website, fill in your details, and complete registration. After approval, you can access your member dashboard."),
        Tuple.Create(
            "How do I contact customer support?",
            "You can reach us via the Contact link in the header, email support, or WhatsApp for quick help with orders and account queries."),
        Tuple.Create(
            "Is my payment information secure?",
            "Yes. Payments are processed through secure payment gateways. Maniraya does not store your full card details on our servers."),
        Tuple.Create(
            "What are wallet rewards and how do I use them?",
            "Members can earn wallet rewards on eligible purchases and activities. Wallet balance can be used for future orders as per active plan rules.")
    };

    public static void EnsureTableAndSeedDefaults()
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                string createSql = @"
                IF OBJECT_ID('tbl_FAQ', 'U') IS NULL
                BEGIN
                    CREATE TABLE tbl_FAQ (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Question NVARCHAR(MAX),
                        Answer NVARCHAR(MAX),
                        Status BIT
                    )
                END";
                objData.RunInsUpDelQuery(createSql);

                DataTable countTable = objData.RunDataTable("SELECT COUNT(*) AS FaqCount FROM tbl_FAQ");
                int count = 0;
                if (countTable != null && countTable.Rows.Count > 0)
                {
                    count = Convert.ToInt32(countTable.Rows[0]["FaqCount"]);
                }
                if (count > 0)
                {
                    return;
                }

                foreach (Tuple<string, string> faq in DefaultFaqs)
                {
                    string question = (faq.Item1 ?? string.Empty).Replace("'", "''");
                    string answer = (faq.Item2 ?? string.Empty).Replace("'", "''");
                    string insertSql = string.Format(
                        "INSERT INTO tbl_FAQ (Question, Answer, Status) VALUES ('{0}', '{1}', 1)",
                        question,
                        answer);
                    objData.RunInsUpDelQuery(insertSql);
                }
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }
    }

    public static DataTable GetActiveFaqs()
    {
        DataTable dt = new DataTable();
        Data objData = new Data();
        try
        {
            EnsureTableAndSeedDefaults();
            objData.StartConnection();
            try
            {
                dt = objData.RunDataTable("SELECT * FROM tbl_FAQ WHERE Status = 1 ORDER BY Id ASC");
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
        }

        return dt ?? new DataTable();
    }
}
