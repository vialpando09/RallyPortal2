//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace RallyPortal
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GalleryId { get; set; }
        public int ImageId { get; set; }
    
        public virtual Gallery Gallery { get; set; }
    }
    
}
