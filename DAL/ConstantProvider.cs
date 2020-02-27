using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Constants
{
    public class ConstantProvider
    {
        public const int exifParamSize = 8;
        public const char downloadMode = '1';
        
        public const string FILE_DOWNLOADING_FAILED = "Downloading of file failed: ";
        public const string FILE_DELETION_FAILED = "Deletion of file failed: ";
        
        public const string serverUploadDirectory = "ImageDirectory";
        public const string fileTypes = "FileType";

        public const string defaultDirectory = "~/Content/Images";
        public const string defaultTypes = "image/jpeg;image/png";
    }
}