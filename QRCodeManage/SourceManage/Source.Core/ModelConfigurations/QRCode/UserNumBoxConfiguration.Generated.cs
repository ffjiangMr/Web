﻿// <autogenerated>
//   This file was generated by T4 code generator ConfigurationCodeScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

// -----------------------------------------------------------------------
//  <copyright file="UserNumBoxConfiguration.Generated.cs">
//      Copyright (c) 2017 TomNet. All rights reserved.
//  </copyright>
//  <last-editor>TomNet</last-editor>
// -----------------------------------------------------------------------

using System;
using TomNet.Data.Entity;
using Source.Model.DbModels.QRCode;


namespace Source.Core.ModelConfigurations.QRCode
{
    /// <summary>
    /// 实体类-数据表映射 -- Source.Model.DbModels.QRCode.UserNumBox
    /// </summary> 
	public partial class UserNumBoxConfiguration : EntityConfigurationBase<UserNumBox, int>
    { 
        /// <summary>
        /// 初始化一个<see cref="UserNumBoxConfiguration"/>类型的新实例
        /// </summary>
        public UserNumBoxConfiguration()
        {
            UserNumBoxConfigurationAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void UserNumBoxConfigurationAppend();
    }
}

