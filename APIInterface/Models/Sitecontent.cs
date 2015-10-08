using Castle.Core.Internal;
using System;

namespace APIInterface.Models
{
    /// <summary>
    /// Client's Site Contents 
    /// </summary>
    public class Sitecontent
    {
        /// <summary>
        /// Title of site 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Logo 
        /// </summary>
        public string CompanyLogo { get; set; }

        /// <summary>
        /// Id 
        /// </summary>
        public short SiteContentId { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyDisplayName { get; set; }

        /// <summary>
        /// Company Slogan 
        /// </summary>
        public string Slogan { get; set; }

        /// <summary>
        /// Banner 1 
        /// </summary>
        public string Banner1 { get; set; }

        /// <summary>
        /// Banner 2 
        /// </summary>
        public string Banner2 { get; set; }

        /// <summary>
        /// Banner 3
        /// </summary>
        public string Banner3 { get; set; }

        /// <summary>
        /// Service Contents 
        /// </summary>
        public string ServiceContents { get; set; }

        /// <summary>
        /// About US contents 
        /// </summary>
        public string AboutusContents { get; set; }

        /// <summary>
        /// Company Address 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Company telephone 
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Company Facebook link
        /// </summary>
        public string FBLink { get; set; }

        /// <summary>
        /// Company twiter LInk 
        /// </summary>
        public string TwiterLink { get; set; }

        /// <summary>
        /// Company's Logo click URL 
        /// </summary>
        public string WebsiteClickURL { get; set; }

        /// <summary>
        /// Background color Company site 
        /// </summary>
        public string BodyBGColor { get; set; }

        /// <summary>
        /// Company Email 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Company URL for searching 
        /// </summary>
        public string CompanyShortName { get; set; }

        /// <summary>
        /// Extra Fields [not under use ]
        /// </summary>
        public string ExtraField1 { get; set; }
        public string ExtraField2 { get; set; }
        public string ExtraField3 { get; set; }
        public string ExtraField4 { get; set; }


        /// <summary>
        /// User domain key 
        /// </summary>
        public long UserDomainKey { get; set; }

        public string CompanyLogoBytes { get; set; }


       public byte[] LogoSourceLocal { get; set; }

       
        public string CompanyLogoImage
        {
            get
            {
                if (LogoSourceLocal == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(LogoSourceLocal);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
            set
            {
                if (value.IsNullOrEmpty() || !value.Contains("base64,"))
                {
                    CompanyLogoBytes = null;
                    return;
                }
                var index = value.IndexOf("base64,", StringComparison.Ordinal);
                LogoSourceLocal = Convert.FromBase64String(value.Substring(index + 7));
            }
        }


        /// <summary>
        /// Image
        /// </summary>
        public string Banner1Bytes { get; set; }
        public byte[] Banner1SourceLocal { get; set; }

        ///// <summary>
        ///// Banner1 Source
        ///// </summary>
        //public string Banner1Source
        //{
        //    get
        //    {
        //        if (Banner1Bytes == null)
        //        {
        //            return string.Empty;
        //        }

        //        string base64 = Convert.ToBase64String(Banner1Bytes);
        //        return string.Format("data:{0};base64,{1}", "image/jpg", base64);
        //    }
        //    set
        //    {
        //        if (value.IsNullOrEmpty() || !value.Contains("base64,"))
        //        {
        //            Banner1Bytes = null;
        //            return;
        //        }
        //        var index = value.IndexOf("base64,", StringComparison.Ordinal);
        //        Banner1Bytes = Convert.FromBase64String(value.Substring(index + 7));
        //    }
        //}




        /// <summary>
        /// Image
        /// </summary>
        public string Banner2Bytes { get; set; }
        public byte[] Banner2SourceLocal { get; set; }
        ///// <summary>
        ///// Banner2 Source
        ///// </summary>
        //public string Banner2Source
        //{
        //    get
        //    {
        //        if (Banner2Bytes == null)
        //        {
        //            return string.Empty;
        //        }

        //        string base64 = Convert.ToBase64String(Banner2Bytes);
        //        return string.Format("data:{0};base64,{1}", "image/jpg", base64);
        //    }
        //    set
        //    {
        //        if (value.IsNullOrEmpty() || !value.Contains("base64,"))
        //        {
        //            Banner2Bytes = null;
        //            return;
        //        }
        //        var index = value.IndexOf("base64,", StringComparison.Ordinal);
        //        Banner2Bytes = Convert.FromBase64String(value.Substring(index + 7));
        //    }
        //}

        public string TitleIcon { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Banner3Bytes { get; set; }
        public byte[] Banner3SourceLocal { get; set; }
        ///// <summary>
        ///// Banner2 Source
        ///// </summary>
        //public string Banner3Source
        //{
        //    get
        //    {
        //        if (Banner3Bytes == null)
        //        {
        //            return string.Empty;
        //        }

        //        string base64 = Convert.ToBase64String(Banner3Bytes);
        //        return string.Format("data:{0};base64,{1}", "image/jpg", base64);
        //    }
        //    set
        //    {
        //        if (value.IsNullOrEmpty() || !value.Contains("base64,"))
        //        {
        //            Banner3Bytes = null;
        //            return;
        //        }
        //        var index = value.IndexOf("base64,", StringComparison.Ordinal);
        //        Banner3Bytes = Convert.FromBase64String(value.Substring(index + 7));
        //    }
        //}
    }
}