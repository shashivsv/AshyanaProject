//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ashyana.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public int RoomTypeID { get; set; }
        public string RoomType { get; set; }
        public string RoomDesc { get; set; }
        public string RoomImage { get; set; }
        public Nullable<int> RoomDelete { get; set; }
        public Nullable<System.DateTime> RoomDeletedon { get; set; }
        public Nullable<int> RoomCreatedby { get; set; }
        public Nullable<System.DateTime> RoomCreatedon { get; set; }
        public Nullable<int> RoomDeletedby { get; set; }
        public Nullable<int> RoomUpdatedby { get; set; }
        public Nullable<System.DateTime> RoomUpdatedon { get; set; }
    }
}
