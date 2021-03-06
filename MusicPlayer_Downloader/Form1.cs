﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using VideoLibrary;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace MusicPlayer_Downloader
{   
    public partial class Form1 : MaterialForm
    {
        private OpenFileDialog openFileDialog1;

        public Form1()
        { 
            //Izgradnja UI
            InitializeComponent();
            var skinmenager = MaterialSkinManager.Instance;
            skinmenager.AddFormToManage(this);
            skinmenager.Theme = MaterialSkinManager.Themes.DARK;
            skinmenager.ColorScheme = new ColorScheme(Primary.BlueGrey700, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.Amber700, TextShade.WHITE);
            //Tab Text
            tabPage1.Text = "Youtube";
            tabPage2.Text = "Music Player";
            tabPage3.Text = "Connect with Facebook";
        }   //UI Dizajn

        private async void materialFlatButton1_Click(object sender, EventArgs e)
        {
            //Skidanje Videa
            using (FolderBrowserDialog fdb = new FolderBrowserDialog() { Description = "Chose your destination" })
            {
                if (fdb.ShowDialog() == DialogResult.OK)
                {
                    var youtube = YouTube.Default;
                    stslabel.Text = "Downloading...";
                    var video = await youtube.GetVideoAsync(Urlbox.Text);
                    File.WriteAllBytes(fdb.SelectedPath + video.FullName, await video.GetBytesAsync());
                    stslabel.Text = "Completed";
                }
            }
        } //Skidanje Videa

        private void Urlbox_Click(object sender, EventArgs e)
        {
            Urlbox.Clear();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            //OpenFileDialog Metoda and choosing Music
            Stream myStream = null;
            openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "MP3 Audio File (*.mp3)|*.mp3| Windows Media File (*.wma)|*.wma|WAV Audio File  (*.wav)|*.wav|All FILES (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog1.FileNames;
                            string[] filename = openFileDialog1.SafeFileNames;

                            for (int i = 0; i < openFileDialog1.SafeFileNames.Count(); i++)
                            {
                                string[] saLvwItem = new string[2];
                                saLvwItem[0] = filename[i];
                                saLvwItem[1] = fileNameAndPath[i];
                                ListViewItem lvi = new ListViewItem(saLvwItem);
                                listView1.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: could not read file from disk");
                }
            }
        } //Izabiranje muzike(OpenFileDialog)

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //double click on a song for play
            {
                string selectedFile = listView1.FocusedItem.SubItems[1].Text;
                axWindowsMediaPlayer1.URL = @selectedFile;
            }
        } //Dupli Clikc(ListVIEW)

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < listView1.Items.Count; i++)
            {

                int ii = 1;
                media = axWindowsMediaPlayer1.newMedia(listView1.Items[i].SubItems[ii].Text);
                playlist.appendItem(media);
                ii++;
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                if (listView1==null)
                {
                    MessageBox.Show("You must select song to play","Problem",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void emailbox_Click(object sender, EventArgs e)
        {
            //emailbox.Clear();
        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            string name;
            name = materialSingleLineTextField1.Text;
            materialLabel4.Text = name;
            materialSingleLineTextField1.Text = "";

        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            materialLabel4.Text = "";
            materialSingleLineTextField1.Text = "";

        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            string stefn = subject_txt.Text;

            string body = textBox2.Text;
            MailMessage mail = new MailMessage();
            SmtpClient server = new SmtpClient("smtp.live.com");
            mail.From = new MailAddress("kika_988ng@hotmail.com");
            mail.To.Add(stefn);
            mail.Subject = subject_txt.Text;
            mail.Body = body;
            server.Port = 587;
            server.Credentials = new System.Net.NetworkCredential("kika_988ng@hotmail.com", "14101998k");
            server.EnableSsl = true;
            server.Send(mail);
            //MessageBox.Show("Sucseccfull Registration,check Inbox for new updates,proceed to Application?");
        }
    }
}
