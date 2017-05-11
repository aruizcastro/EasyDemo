using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace EasyDemo.Droid.Models
{
    public class ListAdapter : BaseAdapter
    {
        Activity context;
        public List<Users> items;

        public ListAdapter(Activity context, List<Users> items) : base() {
            this.context = context;
            this.items = items;
        }

        public override int Count => items.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ItemRow, parent, false)) as LinearLayout;
            var name = view.FindViewById(Resource.Id.textView_Name) as TextView;
            var idu = view.FindViewById(Resource.Id.textView_IdUser) as TextView;
            idu.SetText(" "+ item.Id, TextView.BufferType.Normal);
            name.SetText(" " + item.Name, TextView.BufferType.Normal);
            return view;
        }

        public Users GetItemAtPosition(int position)
        {
            return items[position];
        }
    }
}