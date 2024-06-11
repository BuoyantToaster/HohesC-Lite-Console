/*
 Simple Console tool for OSC.
 Also getting to this point was a pain. This is like the first fucking thing (beyond 'Hello,World') that I program in C#...
 But hey here I go making this and even though it looks like absolute ass, I guess it works?! (so far at least)

 If you read this and decided that you are better then me in c# (which lets face it you probably are) please submit 

 Future Plans are to also add a receive function (maybe even opening a second Console window so the programm can send and receive at the same time idk)
 
 */

using System.Linq.Expressions;
using System.Net;
using Rug.Osc;

//Some formatting for the console Window as well as the initially shown message
Console.Title = "HohesC Lite Console";
Console.ForegroundColor = ConsoleColor.Green;
//Console.WindowHeight = 100;

//Console.WriteLine("Wittle tiny console for sending OSC messages OwO");//If you wead dis and fwought that I am a silly Furry, then you aw indeed cowect ÒwÓ
Console.WriteLine("Welcome to HohesC Lite!");
Console.WriteLine("This is an work in progress console application designed to send and in the future");
Console.WriteLine("also receive OSC messages. As of now it is unfortunately not possible to save and");
Console.WriteLine("load profiles. ");

//address as well as port for for the messageing instance
string ipAddress;
int port;

//temporary variables
bool progRun = true;
string temp;


//list of valid commands
string ipconfig = "ipconfig";
string cmsgi = "custom -i";
string cmsgb = "custom -b";
string cmsgf = "custom -f";
string cmsgs = "custom -s";
string help = "help";
string exit = "exit";

//starting dialog for the programm (This may be removed in later versions)
Console.WriteLine("Please enter the receivers IP address in the format xxx.xxx.xxx.xxx (including dots!)");
Console.WriteLine("If nothing is entered, 127.0.0.1 will be used as a default");
Console.Write(">");

temp = Console.ReadLine();

if (temp.Length > 0)
{
    ipAddress = temp;
}
else
{
    ipAddress = "127.0.0.1";
    Console.WriteLine("IP Address has been set to: " + ipAddress);
}

Console.WriteLine("Please enter the receivers port");
Console.WriteLine("If nothing is entered, 9000 will be used as a default");
Console.Write(">");

temp = Console.ReadLine();

if (temp.Length > 0)
{
    port = int.Parse(temp);
}
else
{
    port = 9000;
    Console.WriteLine("IP Address has been set to: " + port);
}

Console.WriteLine("enter 'help' to see all valid commads");
Console.Write(">");

while (progRun == true)
{
    temp = Console.ReadLine();
    if (temp == ipconfig)
    {
        Console.WriteLine("Please enter new receiver IP address in the format xxx.xxx.xxx.xxx (including dots!)");
        Console.Write(">");
        temp = Console.ReadLine();
        ipAddress = temp;

        Console.WriteLine("Please enter new receiver Port");
        Console.Write(">");
        temp = Console.ReadLine();
        port = int.Parse(temp);

    }
    if (temp == help)
    {
        Console.WriteLine("Current Client data is set to: "+ ipAddress +":"+ port +"");
        Console.WriteLine("");
        Console.WriteLine("enter 'ipconfig' to change IP Address and Port");
        Console.WriteLine("");
        Console.WriteLine("enter 'custom' to send to a custom address");
        Console.WriteLine("");
        Console.WriteLine("enter -i for Int, -b for boolean, -f for float or -s for string");
        Console.WriteLine("");
        Console.WriteLine("enter 'exit' to exit");
        Console.Write(">");
    }
    if (temp == cmsgi)
    {
        customMsg(0);
        Console.Write(">");
    }
    if (temp == cmsgb)
    {
        customMsg(1);
        Console.Write(">");
    }
    if (temp == cmsgf)
    {
        customMsg(2);
        Console.Write(">");
    }
    if (temp == cmsgs)
    {
        customMsg(3);
        Console.Write(">");
    }
    if (temp == exit)
    {
        progRun = false;
    }
}

void customMsg(int opt) //send custom messages to custom address
{
    string oscmsg;

    //custom OSC address
    Console.WriteLine("please enter the OSC Address in the format: /example/OSC/address");
    Console.Write(">");

    temp = Console.ReadLine();
    oscmsg = temp;

    switch (opt)
    {
        case 0:
            Console.WriteLine("Please enter the int you want to send");
            Console.Write(">");
            temp = Console.ReadLine();
            int msgVarI = int.Parse(temp);
            SendOscMessage(ipAddress, port, oscmsg, msgVarI);
            break;

        case 1:
            Console.WriteLine("Please enter the bool you want to send");
            Console.Write(">");
            temp = Console.ReadLine();
            bool msgVarB = bool.Parse(temp);
            SendOscMessage(ipAddress, port, oscmsg, msgVarB);
            break;

        case 2:
            Console.WriteLine("Please enter the float you want to send");
            Console.Write(">");
            temp = Console.ReadLine();
            float msgVarF = float.Parse(temp);
            SendOscMessage(ipAddress, port, oscmsg, msgVarF);
            break;

        case 3:
            Console.WriteLine("Please enter the string you want to send");
            Console.Write(">");
            temp = Console.ReadLine();
            string msgVarS = temp;
            SendOscMessage(ipAddress, port, oscmsg, msgVarS);
            break;
    }
}

void SendOscMessage<T>(string ipAddress, int port, string oscmsg, T messageValue)
{
    IPAddress ip = IPAddress.Parse(ipAddress);
    var sendMessage = new OscSender(ip, 0, port);

    try
    {
        sendMessage.Connect();
        var inputMessage = new OscMessage(oscmsg, messageValue);
        sendMessage.Send(inputMessage);
        Console.WriteLine("Sent successfully");
        sendMessage.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}
