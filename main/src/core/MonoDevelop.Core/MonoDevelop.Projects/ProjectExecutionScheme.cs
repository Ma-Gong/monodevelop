﻿//
// ProjectExecutionScheme.cs
//
// Author:
//       Lluis Sanchez Gual <lluis@xamarin.com>
//
// Copyright (c) 2016 Xamarin, Inc (http://www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using MonoDevelop.Projects.MSBuild;

namespace MonoDevelop.Projects
{
	public class ProjectExecutionScheme: ExecutionScheme
	{
		IPropertySet properties;
		MSBuildPropertyGroup mainPropertyGroup;

		public ProjectExecutionScheme (string name): base (name)
		{
		}

		public new Project ParentItem {
			get { return (Project)base.ParentItem; }
		}

		internal protected virtual void Read (IPropertySet pset)
		{
			properties = pset;
			pset.ReadObjectProperties (this, GetType (), true);
		}

		internal protected virtual void Write (IPropertySet pset)
		{
			pset.WriteObjectProperties (this, GetType (), true);
		}

		/// <summary>
		/// Property set where the properties for this configuration are defined.
		/// </summary>
		public IPropertySet Properties {
			get {
				return properties ?? MainPropertyGroup;
			}
			internal set {
				properties = value;
			}
		}

		internal MSBuildPropertyGroup MainPropertyGroup {
			get {
				if (mainPropertyGroup == null) {
					if (ParentItem == null)
						mainPropertyGroup = new MSBuildPropertyGroup ();
					else
						mainPropertyGroup = ParentItem.MSBuildProject.CreatePropertyGroup ();
					mainPropertyGroup.IgnoreDefaultValues = true;
				}
				return mainPropertyGroup;
			}
			set {
				mainPropertyGroup = value;
				mainPropertyGroup.IgnoreDefaultValues = true;
			}
		}

		internal MSBuildProjectInstance ProjectInstance { get; set; }
	}
}
