using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project04
{
    public partial class InfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["mediaType"].Equals("music"))
            {
                Response.Write("<p>Track: " + Session["trackName"] + "</p>");
                Response.Write("<p>Artist: " + Session["artistName"] + "</p>");
                Response.Write("<p>Album: " + Session["collectionName"] + "</p>");
                Response.Write("<p>Track Price: $" + Session["trackPrice"] + "</p>");
                Response.Write("<p>Album Price: $" + Session["collectionPrice"] + "</p>");
            }

            else if (Session["mediaType"].Equals("movie"))
            {

                Response.Write("<p>Movie Title: " + Session["trackName"] + "</p>");
                Response.Write("<p>Advisory Rating: " + Session["contentAdvisoryRating"] + "</p>");
                Response.Write("<p>Genre: " + Session["primaryGenreName"] + "</p>");
                Response.Write("<p>Price: $" + Session["trackHdPrice"] + "</p>");
                Response.Write("<p>Description: " + Session["longDescription"] + "</p>");


            }

            else if (Session["mediaType"].Equals("software"))
            {
                Response.Write("<p>Title: " + Session["trackName"] + "</p>");
                Response.Write("<p>Publisher: " + Session["artistName"] + "</p>");
                Response.Write("<p>Price: $" + Session["formattedPrice"] + "</p>");
                Response.Write("<p>Supported Devices: " + Session["supportedDevices"] + "</p>");
                Response.Write("<p>Genres: " + Session["genres"] + "</p>");
                Response.Write("<p>Description: " + Session["description"] + "</p>");
                Response.Write("<img src='" + Session["screenshotUrl1"] + "'/>");
                Response.Write("<img src='" + Session["screenshotUrl2"] + "'/>");


            }
        }
    }
}