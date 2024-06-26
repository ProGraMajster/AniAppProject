using System.Numerics;

namespace AniAppProject.UI;

public partial class StatSeriesView : ContentView
{
    List<Data.Stats> Stats;
    int Max = 0;
	public StatSeriesView(List<Data.Stats> stats)
	{
		InitializeComponent();
        Stats= stats;

    }

    private void ContentView_Loaded(object sender, EventArgs e)
    {
		if(Stats == null)
        {
            return;
        }
        int max = 0;

        foreach(Data.Stats item in Stats)
        {
            if(item != null)
            {
                setInfoLbl(item);
                max += item.count;
            }
        }


        foreach(Data.Stats item in Stats)
        {
            if(item != null)
            {
                var r= (int)Math.Round((double)(100 * item.count) / max);
                setInfoPro(item,r);
            }
        }
    }

    void setInfoLbl(Data.Stats item)
    {
        if(item == null)
        {
            return;
        }

        if(item.status== 1)
        {
            stat_1_lblCount.Text = item.count.ToString();
        }
        else if(item.status== 2)
        {
            stat_2_lblCount.Text = item.count.ToString();
        }
        else if(item.status== 3)
        {
            stat_3_lblCount.Text = item.count.ToString();
        }
        else if(item.status== 4)
        {
            stat_4_lblCount.Text = item.count.ToString();
        }
        else if(item.status== 5)
        {
            stat_5_lblCount.Text = item.count.ToString();
        }
    }

    void setInfoPro(Data.Stats item, int v)
    {
        if(item == null)
        {
            return;
        }

        double z = (double)v;

        if (item.status== 1)
        {
            stat_1_progressbar.Progress = z;
        }
        else if(item.status== 2)
        {
            stat_2_progressbar.Progress = z;
        }
        else if(item.status== 3)
        {
            stat_3_progressbar.Progress = z;
        }
        else if(item.status== 4)
        {
            stat_4_progressbar.Progress = z;
        }
        else if(item.status== 5)
        {
            stat_5_progressbar.Progress = z;
        }
    }
}