using System;
using Castle.Core.Internal;

namespace APIInterface.Models
{
    public class Sitecontent
    {
        public string Title { get; set; }
        public string CompanyLogo { get; set; }
        public short SiteContentId { get; set; }
        public string CompanyDisplayName { get; set; }
        public string Slogan { get; set; }
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
        public string Banner3 { get; set; }
        public string ServiceContents { get; set; }
        public string AboutusContents { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string FBLink { get; set; }
        public string TwiterLink { get; set; }
        public string WebsiteClickURL { get; set; }
        public string BodyBGColor { get; set; }
        public string Email { get; set; }
        public string CompanyShortName { get; set; }
        public string ExtraField1 { get; set; }
        public string ExtraField2 { get; set; }
        public string ExtraField3 { get; set; }
        public string ExtraField4 { get; set; }
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