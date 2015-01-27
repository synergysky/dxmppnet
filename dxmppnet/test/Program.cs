﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Test
{
	class MainClass
	{
		class EchoBot
		{
			DXMPP.Connection Connection;

			public EchoBot()
			{
				Connection = new DXMPP.Connection("deusexmachinae.se", 5222, 
						new DXMPP.JID("dxmpp@users/net"), "dxmpp");

				Connection.OnStanza += HandleOnStanzaCallback;
				Connection.Connect();
			}

			void HandleOnStanzaCallback (DXMPP.Stanza Data)
			{
				var BodyElement = Data.Message.XPathSelectElement ("//body");
				if (BodyElement == null)
					return;

				DXMPP.Stanza EchoMessage = new DXMPP.Stanza ();
				EchoMessage.To = Data.From;
				EchoMessage.From = Data.To;
				EchoMessage.Message.Add (BodyElement);
				Connection.SendStanza (EchoMessage);
			}
		}


		public static void Main (string[] args)
		{
			EchoBot T = new EchoBot ();
		}
	}
}