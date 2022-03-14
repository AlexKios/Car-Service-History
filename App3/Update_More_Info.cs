using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App3
{
    [Activity(Label = "Update Note")]
    public class Update_More_Info : Activity
    {
        EditText note, km;
        Button save;
        DataBaseNotes dataBaseNotes = new DataBaseNotes();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.update_more_info);

            note = FindViewById<EditText>(Resource.Id.updatedNote);
            km = FindViewById<EditText>(Resource.Id.updatedkm);
            save = FindViewById<Button>(Resource.Id.saveUpdate);
            save.Click += onSaveUpdate;

            var allNotes = dataBaseNotes.GetTable().ToList();
            var notes = allNotes[Choose_Car.GetId()].GetNotes().ToList();

            note.Text = notes[Show_More_Info.GetNoteId()].Text;
            km.Text = notes[Show_More_Info.GetNoteId()].Km;

        }
      
        private async void onSaveUpdate(Object sender, EventArgs e)
        {
            if (note.Text == "")
            {
                Toast.MakeText(this, "No note entered", ToastLength.Long).Show();
            }
            else
            {
                var allNotes = dataBaseNotes.GetTable().ToList();
                var notes = allNotes[Choose_Car.GetId()].GetNotes().ToList();

                notes[Show_More_Info.GetNoteId()].Text = note.Text;
                notes[Show_More_Info.GetNoteId()].Km = km.Text;
                notes[Show_More_Info.GetNoteId()].Date = notes[Show_More_Info.GetNoteId()].Date;

                allNotes[Choose_Car.GetId()].UpdateNote(notes[Show_More_Info.GetNoteId()]);
                dataBaseNotes.Update(allNotes[Choose_Car.GetId()]);
             
                var intent = new Intent(this, typeof(Show_More_Info));
                this.StartActivity(intent);

            }
        }
    }
}