﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace MatoRecipe_Model.Model
{
    /// <summary>
    /// 实体类cook_classify。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("cook_classify")]
    [Serializable]
    public partial class cook_classify : Entity
    {
        #region Model
		private int _Id;
		private int? _cook_class;
		private string _description;
		private string _name;
		private int? _seq;
		private string _title;
		private DateTime _create_time;

		/// <summary>
		/// 
		/// </summary>
		[Field("Id")]
		public int Id
		{
			get{ return _Id; }
			set
			{
				this.OnPropertyValueChange("Id");
				this._Id = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("cook_class")]
		public int? cook_class
		{
			get{ return _cook_class; }
			set
			{
				this.OnPropertyValueChange("cook_class");
				this._cook_class = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("description")]
		public string description
		{
			get{ return _description; }
			set
			{
				this.OnPropertyValueChange("description");
				this._description = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("name")]
		public string name
		{
			get{ return _name; }
			set
			{
				this.OnPropertyValueChange("name");
				this._name = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("seq")]
		public int? seq
		{
			get{ return _seq; }
			set
			{
				this.OnPropertyValueChange("seq");
				this._seq = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("title")]
		public string title
		{
			get{ return _title; }
			set
			{
				this.OnPropertyValueChange("title");
				this._title = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("create_time")]
		public DateTime create_time
		{
			get{ return _create_time; }
			set
			{
				this.OnPropertyValueChange("create_time");
				this._create_time = value;
			}
		}
		#endregion

		#region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.Id,
			};
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.Id,
				_.cook_class,
				_.description,
				_.name,
				_.seq,
				_.title,
				_.create_time,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._Id,
				this._cook_class,
				this._description,
				this._name,
				this._seq,
				this._title,
				this._create_time,
			};
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

		#region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
			/// <summary>
			/// * 
			/// </summary>
			public readonly static Field All = new Field("*", "cook_classify");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Id = new Field("Id", "cook_classify", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field cook_class = new Field("cook_class", "cook_classify", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field description = new Field("description", "cook_classify", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field name = new Field("name", "cook_classify", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field seq = new Field("seq", "cook_classify", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field title = new Field("title", "cook_classify", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field create_time = new Field("create_time", "cook_classify", "");
        }
        #endregion
	}
}