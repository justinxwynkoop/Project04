using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace Project04
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMedia.Items.Add(new ListItem("Music", "music"));
                ddlMedia.Items.Add(new ListItem("Movie", "movie"));
                ddlMedia.Items.Add(new ListItem("Software", "software"));

                tbxSearch.Text = "";
            }



            string text = tbxSearch.Text;
            string searchTerm = text.Replace(" ", "+");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://itunes.apple.com/search?term=" + searchTerm + "&media=" + ddlMedia.SelectedValue);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rdr = new StreamReader(response.GetResponseStream());
            string json = rdr.ReadToEnd();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(iTunes));
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            iTunes searchResults = (iTunes)js.ReadObject(ms);

            Label label = new Label();
            //label.Text = searchResults.ResultCount.ToString();
            this.Controls.Add(label);
            Table table = new Table();
            if (ddlMedia.SelectedValue == "music")
            {
                foreach (Result r in searchResults.Results)
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();

                    Label trackNameLabel = new Label();
                    trackNameLabel.Text = r.TrackName;
                    Label collectionNameLabel = new Label();
                    collectionNameLabel.Text = r.CollectionName;
                    Image artworkImage = new Image();
                    artworkImage.ImageUrl = r.ArtworkUrl60;

                    LinkButton btnInfo = new LinkButton();
                    btnInfo.Text = "More Info";
                    btnInfo.Click += (sender2, args) =>
                    {
                        Session["mediaType"] = "music";
                        Session["trackName"] = r.TrackName;
                        Session["artistName"] = r.ArtistName;
                        Session["collectionName"] = r.CollectionName;
                        Session["tracokPrice"] = r.TrackPrice;
                        Session["collectionPrice"] = r.CollectionPrice;
                        Response.Redirect("MoreInfo.aspx");
                    };


                    cell1.Controls.Add(trackNameLabel);
                    cell2.Controls.Add(collectionNameLabel);
                    cell3.Controls.Add(artworkImage);
                    cell4.Controls.Add(btnInfo);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    row.Controls.Add(cell3);
                    row.Controls.Add(cell4);
                    table.Controls.Add(row);

                }
            }
            else if (ddlMedia.SelectedValue == "movie")
            {
                foreach (Result r in searchResults.Results)
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    Label trackNameLabel = new Label();
                    trackNameLabel.Text = r.TrackName;
                    Label contentAdvisoryRatingLabel = new Label();
                    contentAdvisoryRatingLabel.Text = r.ContentAdvisoryRating;
                    Image artworkImage = new Image();
                    artworkImage.ImageUrl = r.ArtworkUrl60;

                    LinkButton btnInfo = new LinkButton();
                    btnInfo.Text = "More Info";
                    btnInfo.Click += (sender2, args) =>
                    {
                        Session["mediaType"] = "movie";
                        Session["trackName"] = r.TrackName;
                        Session["contentAdvisoryRating"] = r.ContentAdvisoryRating;
                        Session["longDescription"] = r.LongDescription;
                        Session["primaryGenreName"] = r.PrimaryGenreName;
                        Session["trackHdPrice"] = r.TrackHdPrice;
                        Response.Redirect("MoreInfo.aspx");
                    };


                    cell1.Controls.Add(trackNameLabel);
                    cell2.Controls.Add(contentAdvisoryRatingLabel);
                    cell3.Controls.Add(artworkImage);
                    cell4.Controls.Add(btnInfo);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    row.Controls.Add(cell3);
                    row.Controls.Add(cell4);
                    table.Controls.Add(row);

                }
            }
            else if (ddlMedia.SelectedValue == "software")
            {
                foreach (Result r in searchResults.Results)
                {

                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();

                    Label trackNameLabel = new Label();
                    trackNameLabel.Text = r.TrackName;
                    Label artistNameLabel = new Label();
                    artistNameLabel.Text = r.ArtistName;
                    Label priceLabel = new Label();
                    priceLabel.Text = r.FormattedPrice;
                    Image artworkImage = new Image();
                    artworkImage.ImageUrl = r.ArtworkUrl60;

                    string devicesList = "";
                    foreach (string device in r.SupportedDevices)
                    {
                        devicesList += device + ", ";
                    }

                    string genreList = "";
                    foreach (string genre in r.Genres)
                    {
                        genreList += genre + ", ";
                    }


                    LinkButton btnInfo = new LinkButton();
                    btnInfo.Text = "More Info";
                    btnInfo.Click += (sender2, args) =>
                    {
                        Session["mediaType"] = "software";
                        Session["trackName"] = r.TrackName;
                        Session["artistName"] = r.ArtistName;
                        Session["formattedPrice"] = r.FormattedPrice;
                        Session["supportedDevices"] = devicesList;
                        Session["description"] = r.Description;
                        Session["genres"] = genreList;
                        Session["screenshotUrl1"] = r.ScreenshotUrls[0];
                        Session["screenshotUrl2"] = r.ScreenshotUrls[1];
                        Response.Redirect("MoreInfo.aspx");
                    };


                    cell1.Controls.Add(trackNameLabel);
                    cell2.Controls.Add(priceLabel);
                    cell3.Controls.Add(artistNameLabel);
                    cell4.Controls.Add(artworkImage);
                    cell5.Controls.Add(btnInfo);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    row.Controls.Add(cell3);
                    row.Controls.Add(cell4);
                    row.Controls.Add(cell5);
                    table.Controls.Add(row);


                }
            }

            pnlTable.Controls.Add(table);



        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            string text = tbxSearch.Text;
            string searchTerm = text.Replace(" ", "+");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://itunes.apple.com/search?term=" + searchTerm + "&media=" + ddlMedia.SelectedValue);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rdr = new StreamReader(response.GetResponseStream());
            string json = rdr.ReadToEnd();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(iTunes));
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            iTunes searchResults = (iTunes)js.ReadObject(ms);

            Label label = new Label();
            label.Text = searchResults.ResultCount.ToString();
            this.Controls.Add(label);
            Table table = new Table();
            if (ddlMedia.SelectedValue == "music")
            {
                foreach (Result r in searchResults.Results)
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();

                    Label trackNameLabel = new Label();
                    trackNameLabel.Text = r.TrackName;
                    Label collectionNameLabel = new Label();
                    collectionNameLabel.Text = r.CollectionName;
                    Image artworkImage = new Image();
                    artworkImage.ImageUrl = r.ArtworkUrl60;

                    LinkButton btnInfo = new LinkButton();
                    btnInfo.Text = "More Info";
                    btnInfo.Click += (sender2, args) =>
                    {
                        Session["mediaType"] = "music";
                        Session["trackName"] = r.TrackName;
                        Session["artistName"] = r.ArtistName;
                        Session["collectionName"] = r.CollectionName;
                        Session["tracokPrice"] = r.TrackPrice;
                        Session["collectionPrice"] = r.CollectionPrice;
                        Response.Redirect("MoreInfo.aspx");
                    };


                    cell1.Controls.Add(trackNameLabel);
                    cell2.Controls.Add(collectionNameLabel);
                    cell3.Controls.Add(artworkImage);
                    cell4.Controls.Add(btnInfo);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    row.Controls.Add(cell3);
                    row.Controls.Add(cell4);
                    table.Controls.Add(row);

                }
            }
            else if (ddlMedia.SelectedValue == "movie")
            {
                foreach (Result r in searchResults.Results)
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    Label trackNameLabel = new Label();
                    trackNameLabel.Text = r.TrackName;
                    Label contentAdvisoryRatingLabel = new Label();
                    contentAdvisoryRatingLabel.Text = r.ContentAdvisoryRating;
                    Image artworkImage = new Image();
                    artworkImage.ImageUrl = r.ArtworkUrl60;

                    LinkButton btnInfo = new LinkButton();
                    btnInfo.Text = "More Info";
                    btnInfo.Click += (sender2, args) =>
                    {
                        Session["mediaType"] = "movie";
                        Session["trackName"] = r.TrackName;
                        Session["contentAdvisoryRating"] = r.ContentAdvisoryRating;
                        Session["longDescription"] = r.LongDescription;
                        Session["primaryGenreName"] = r.PrimaryGenreName;
                        Session["trackHdPrice"] = r.TrackHdPrice;
                        Response.Redirect("MoreInfo.aspx");
                    };


                    cell1.Controls.Add(trackNameLabel);
                    cell2.Controls.Add(contentAdvisoryRatingLabel);
                    cell3.Controls.Add(artworkImage);
                    cell4.Controls.Add(btnInfo);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    row.Controls.Add(cell3);
                    row.Controls.Add(cell4);
                    table.Controls.Add(row);




                }
            }
            else if (ddlMedia.SelectedValue == "software")
            {
                foreach (Result r in searchResults.Results)
                {

                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();

                    Label trackNameLabel = new Label();
                    trackNameLabel.Text = r.TrackName;
                    Label artistNameLabel = new Label();
                    artistNameLabel.Text = r.ArtistName;
                    Label priceLabel = new Label();
                    priceLabel.Text = r.FormattedPrice;
                    Image artworkImage = new Image();
                    artworkImage.ImageUrl = r.ArtworkUrl60;

                    string devicesList = "";
                    foreach (string device in r.SupportedDevices)
                    {
                        devicesList += device + ", ";
                    }

                    string genreList = "";
                    foreach (string genre in r.Genres)
                    {
                        genreList += genre + ", ";
                    }


                    LinkButton btnInfo = new LinkButton();
                    btnInfo.Text = "More Info";
                    btnInfo.Click += (sender2, args) =>
                    {
                        Session["mediaType"] = "software";
                        Session["trackName"] = r.TrackName;
                        Session["artistName"] = r.ArtistName;
                        Session["formattedPrice"] = r.FormattedPrice;
                        Session["supportedDevices"] = devicesList;
                        Session["description"] = r.Description;
                        Session["genres"] = genreList;
                        Session["screenshotUrl1"] = r.ScreenshotUrls[0];
                        Session["screenshotUrl2"] = r.ScreenshotUrls[1];
                        Response.Redirect("MoreInfo.aspx");
                    };


                    cell1.Controls.Add(trackNameLabel);
                    cell2.Controls.Add(priceLabel);
                    cell3.Controls.Add(artistNameLabel);
                    cell4.Controls.Add(artworkImage);
                    cell5.Controls.Add(btnInfo);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    row.Controls.Add(cell3);
                    row.Controls.Add(cell4);
                    row.Controls.Add(cell5);
                    table.Controls.Add(row);


                }
            }

            pnlTable.Controls.Add(table);
        }




    }
    [DataContract]
    public class Result
    {

        [DataMember(Name = "wrapperType")]
        public string WrapperType { get; set; }

        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "artistId")]
        public int ArtistId { get; set; }

        [DataMember(Name = "collectionId")]
        public int CollectionId { get; set; }

        [DataMember(Name = "trackId")]
        public int TrackId { get; set; }

        [DataMember(Name = "artistName")]
        public string ArtistName { get; set; }

        [DataMember(Name = "collectionName")]
        public string CollectionName { get; set; }

        [DataMember(Name = "trackName")]
        public string TrackName { get; set; }

        [DataMember(Name = "collectionCensoredName")]
        public string CollectionCensoredName { get; set; }

        [DataMember(Name = "trackCensoredName")]
        public string TrackCensoredName { get; set; }

        [DataMember(Name = "artistViewUrl")]
        public string ArtistViewUrl { get; set; }

        [DataMember(Name = "collectionViewUrl")]
        public string CollectionViewUrl { get; set; }

        [DataMember(Name = "trackViewUrl")]
        public string TrackViewUrl { get; set; }

        [DataMember(Name = "previewUrl")]
        public string PreviewUrl { get; set; }

        [DataMember(Name = "artworkUrl30")]
        public string ArtworkUrl30 { get; set; }

        [DataMember(Name = "artworkUrl60")]
        public string ArtworkUrl60 { get; set; }

        [DataMember(Name = "artworkUrl100")]
        public string ArtworkUrl100 { get; set; }

        [DataMember(Name = "collectionPrice")]
        public double CollectionPrice { get; set; }

        [DataMember(Name = "trackPrice")]
        public double TrackPrice { get; set; }

        [DataMember(Name = "releaseDate")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "collectionExplicitness")]
        public string CollectionExplicitness { get; set; }

        [DataMember(Name = "trackExplicitness")]
        public string TrackExplicitness { get; set; }

        [DataMember(Name = "discCount")]
        public int DiscCount { get; set; }

        [DataMember(Name = "discNumber")]
        public int DiscNumber { get; set; }

        [DataMember(Name = "trackCount")]
        public int TrackCount { get; set; }

        [DataMember(Name = "trackNumber")]
        public int TrackNumber { get; set; }

        [DataMember(Name = "trackTimeMillis")]
        public int TrackTimeMillis { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "primaryGenreName")]
        public string PrimaryGenreName { get; set; }

        [DataMember(Name = "isStreamable")]
        public bool IsStreamable { get; set; }

        [DataMember(Name = "contentAdvisoryRating")]
        public string ContentAdvisoryRating { get; set; }

        [DataMember(Name = "shortDescription")]
        public string ShortDescription { get; set; }

        [DataMember(Name = "longDescription")]
        public string LongDescription { get; set; }

        [DataMember(Name = "trackHdPrice")]
        public double TrackHdPrice { get; set; }

        [DataMember(Name = "collectionArtistName")]
        public string CollectionArtistName { get; set; }

        [DataMember(Name = "supportedDevices")]
        public IList<string> SupportedDevices { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "genres")]
        public IList<string> Genres { get; set; }

        [DataMember(Name = "screenshotUrls")]
        public IList<string> ScreenshotUrls { get; set; }

        [DataMember(Name = "formattedPrice")]
        public string FormattedPrice { get; set; }
    }

    [DataContract]
    public class iTunes
    {

        [DataMember(Name = "resultCount")]
        public int ResultCount { get; set; }

        [DataMember(Name = "results")]
        public IList<Result> Results { get; set; }
    }
}