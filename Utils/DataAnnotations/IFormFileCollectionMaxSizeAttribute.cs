using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionCore.Utils.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class IFormFileCollectionMaxSizeAttribute : ValidationAttribute
    {
        private int? _maxSize;

        public IFormFileCollectionMaxSizeAttribute()
        { }

        public IFormFileCollectionMaxSizeAttribute(int maxSize) =>
            _maxSize = maxSize;

        public int MaxSize
        {
            get
            {
                return _maxSize == null ? 12 * 1024 * 1024 : (int)_maxSize;
            }
            set
            {
                _maxSize = value;
            }
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is ICollection<IFormFile> files)
            {
                long size = 0;

                if ((size = files.Sum(file => file.Length)) > MaxSize)
                {
                    return new ValidationResult(FormatErrorMessage("size = " + Math.Round(size * Math.Pow(10, -6), 2) + " MB"));
                }
            }

            return ValidationResult.Success;
        }
                
        public override string FormatErrorMessage(string name) =>
            string.Format(Resources.Culture, ErrorMessageString, name, MaxSize);
    }
}
