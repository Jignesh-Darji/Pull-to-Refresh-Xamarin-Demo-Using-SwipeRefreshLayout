using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace PullToRefreshDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V4.Widget.SwipeRefreshLayout pullToRefresh;
        private ListView listview;
        private List<string> items;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            pullToRefresh = FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.pullToRefresh);
            
            //set the color scheme of your SwipeRefreshLayout
            pullToRefresh.SetColorSchemeResources(Resource.Color.Red, Resource.Color.Blue, Resource.Color.Yellow, Resource.Color.Green);

            listview =FindViewById<ListView> (Resource.Id.listView1);

            //Add dummy items in the list
            items = new List<string>();
            items.Add("Item 1");
            items.Add("Item 2");
            items.Add("Item 3");
            items.Add("Item 4");
            items.Add("Item 5");
            items.Add("Item 6");
            items.Add("Item 7");
            items.Add("Item 8");
            items.Add("Item 9");
            items.Add("Item 10");
            updateListview(items);

            pullToRefresh.Refresh += PullToRefresh_Refresh;
        }
        private void PullToRefresh_Refresh(object sender, EventArgs e)
        {
            //Data Refresh Place  
            //You can replace this code with your logic for refresh the list
            Task.Run(() => {
                Thread.Sleep(3000);
                if(items !=null && items.Count>0)
                {
                    items.Add("Item " + (items.Count + 1));// Add new item on refresh
                }
                else
                {
                    items = new List<string>();
                    items.Add("Item 1");
                }
                updateListview(items);
                pullToRefresh.Refreshing = false;
            });
        }
        private void updateListview(List<string> items)
        {
            RunOnUiThread(() => {
            ArrayAdapter<string> liststring = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, items);
            listview.Adapter = liststring;
            });

        }
    }
}