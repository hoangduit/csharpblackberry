﻿using net.rim.device.api.ui.container;
using net.rim.device.api.ui.component;
using net.rim.device.api.ui;

namespace HelloWorldCSDemo
{
    public class HelloWorldMainScreen: MainScreen
    {
        public HelloWorldMainScreen()
        {
            // Set the displayed title of the screen       
            base.setTitle(getAppTitle());

            // Add a read only text field (RichTextField) to the screen.  The
            // RichTextField is focusable by default. Here we provide a style
            // parameter to make the field non-focusable.
            base.add(new RichTextField("Hello World!", Field.NON_FOCUSABLE));
        }

        /***
         * Displays a dialog box to the user with the text "Goodbye!" when the
         * application is closed.
         */
        public override void close()
        {
            // Display a farewell message before closing the application
            Dialog.alert("Goodbye!");
            base.close();
        }

        /// <summary>
        /// Return the application title
        /// </summary>
        /// <returns></returns>
        internal string getAppTitle()
        {
            return "Hello World Demo";
        }
    }
}