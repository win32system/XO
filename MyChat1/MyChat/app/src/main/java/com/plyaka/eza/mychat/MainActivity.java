package com.plyaka.eza.mychat;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.*;
import com.google.gson.Gson;
import org.java_websocket.client.WebSocketClient;
import org.java_websocket.handshake.ServerHandshake;

import java.net.URI;
import java.net.URISyntaxException;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    Button btnLogin, btnReg, btnInvite, btnRefresh, bt1, bt2, bt3, bt4, bt5, bt6, bt7, bt8, bt9;
    EditText etPassword, etLogin, etEmail;
    ListView etClients;
    TextView tvName;
    private WebSocketClient mWebSocketClient;
    final Gson gson = new Gson();

    Request content;
    Listener listener = new Listener();

    String[] names;
    String roomNumber;
    private static final String TAG = "MyActivity";

    public void setTvName(String tvName) {
        this.tvName.setText(tvName);
    }

    public void setEtClients(String[] names) {
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_list_item_1, names);
        etClients.setAdapter(adapter);

        this.names = names;
    }
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        connectWebSocket();
        btnLogin = (Button) findViewById(R.id.btnLogin);
        btnReg = (Button) findViewById(R.id.btnReg);
        btnInvite = (Button) findViewById(R.id.btnInvite);
        btnRefresh = (Button) findViewById(R.id.btnRefresh);
        bt1 = (Button) findViewById(R.id.bt1);
        bt2 = (Button) findViewById(R.id.bt2);
        bt3 = (Button) findViewById(R.id.bt3);
        bt4 = (Button) findViewById(R.id.bt4);
        bt5 = (Button) findViewById(R.id.bt5);
        bt6 = (Button) findViewById(R.id.bt6);
        bt7 = (Button) findViewById(R.id.bt7);
        bt8 = (Button) findViewById(R.id.bt8);
        bt9 = (Button) findViewById(R.id.bt9);

        etPassword = (EditText)  findViewById(R.id.etPassword);
        etLogin = (EditText)  findViewById(R.id.etLogin);
        etEmail = (EditText)  findViewById(R.id.etEmail);
        etClients = (ListView)  findViewById(R.id.etClients);
        etClients.setChoiceMode(ListView.CHOICE_MODE_SINGLE);

        tvName = (TextView)  findViewById(R.id.tvName);

        btnLogin.setOnClickListener(this);
        btnReg.setOnClickListener(this);
        btnInvite.setOnClickListener(this);
        btnRefresh.setOnClickListener(this);


    }

    public  void goPlaying(Object [] play)
    {
        content = new Request("HandShake", "Ok",new Object[]{play[0], "XO"});
        mWebSocketClient.send(gson.toJson(content));
    }

    public  void start(Object arg) {
        Object[] args = new Gson().fromJson(arg.toString(), Object[].class);
        roomNumber = args[0].toString();
        content = new Request("Game", "Start", arg);
        mWebSocketClient.send(gson.toJson(content));
    }

    public void  move(Object[] args) {

        content = new Request("Game", "Move",args);
        mWebSocketClient.send(gson.toJson(content));
    }

    @Override
    public void onClick(View v) {

        {
            switch (v.getId()) {
                case R.id.btnLogin:

                    content = new Request("Auth", "LogIn",new Object[]{etLogin.getText(), etPassword.getText()});
                    mWebSocketClient.send(gson.toJson(content));
                    break;

                case R.id.btnReg:
                    content = new Request("Auth", "Registration",new Object[]{etLogin.getText(), etPassword.getText(), etEmail.getText() });
                    mWebSocketClient.send(gson.toJson(content));

                    break;

                case R.id.btnInvite:
                    Object player = GetSelectedPlayer();
                    content = new Request("HandShake", "Invite",new Object[]{player, "XO"});
                    mWebSocketClient.send(gson.toJson(content));

                    break;

                case R.id.btnRefresh:
                    content = new Request("Lobby", "refreshClients",null);
                    mWebSocketClient.send(gson.toJson(content));

                    break;


                case R.id.bt1 :
                    move(new Object[]{roomNumber, 0, 0});
                    break;
                case R.id.bt2 :
                    move(new Object[]{roomNumber, 0, 1});

                    break;
                case R.id.bt3 :
                    move(new Object[]{roomNumber, 0, 2});

                    break;
                case R.id.bt4 :
                    move(new Object[]{roomNumber, 1, 0});

                    break;
                case R.id.bt5 :
                    move(new Object[]{roomNumber, 1, 1});

                    break;
                case R.id.bt6 :
                    move(new Object[]{roomNumber, 1, 2});

                    break;
                case R.id.bt7 :
                    move(new Object[]{roomNumber, 2, 0});

                    break;
                case R.id.bt8 :
                    move(new Object[]{roomNumber, 2, 1});

                    break;
                case R.id.bt9 :
                    move(new Object[]{roomNumber, 2, 2});

                    break;

            }
        }
    }
    private  Object GetSelectedPlayer()
    {
        return names[etClients.getCheckedItemPosition()];
    }

    private void connectWebSocket() {
        URI uri;

//192.168.0.108
        try {
            uri = new URI("ws://192.168.1.100:8888/");//"ws://:8080/");/*10.0.2.2*/ //
        } catch (URISyntaxException e) {
            e.printStackTrace();
            etLogin.setText(e.toString());
            return;
        }

        mWebSocketClient = new WebSocketClient(uri) {
            @Override
            public void onOpen(ServerHandshake serverHandshake) {


                Log.i("Websocket", "Opened");

            }

            @Override
            public void onMessage(String s) {
                final String message = s;
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        listener.getMessage(message);
                        Log.i("Websocket", message);
                    }
                });
            }

            @Override
            public void onClose(int i, String s, boolean b) {
                Log.v(TAG, "Closed " +s);
                Log.i("Websocket", "Closed " + s);
            }

            @Override
            public void onError(Exception e) {

                etPassword.setText(e.getMessage());
                Log.v(TAG, "Error " + e.getMessage());
                Log.i("Websocket", "Error " + e.getMessage());
            }
        };
        mWebSocketClient.connect();
    }

    public  void showAlert(String message)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
        builder.setTitle("Важное сообщение!")
                .setMessage(message)
                .setCancelable(false)
                .setNegativeButton("ОК",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.cancel();
                            }
                        });
        AlertDialog alert = builder.create();
        alert.show();
    }

    boolean ret = false;
    public  boolean showConfirm(String message)
    {

        AlertDialog.Builder alertDialog = new AlertDialog.Builder(MainActivity.this);
        alertDialog.setTitle(" ");

        alertDialog.setMessage("Player " + message + "wants to play with you");

        alertDialog.setPositiveButton("OK", new DialogInterface.OnClickListener() {

            @Override
            public void onClick(DialogInterface dialog, int which) {
                ret = true;
            }
        });

        alertDialog.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                ret = false;
                dialog.cancel();
            }
        });

        alertDialog.show();
        return  ret;
    }

    public  void ShowGame()
    {
        Button[] gamebuttons = new Button[]{bt1,bt2, bt3, bt4, bt5,bt6, bt7, bt8, bt9 };

        for(Button b : gamebuttons)
        {
            b.setVisibility(View.VISIBLE);
        }

        Button[] lobbybuttons = new Button[]{btnInvite,btnLogin, btnRefresh, btnReg};

        for(Button b : lobbybuttons)
        {
            b.setVisibility(View.GONE);
        }

        etClients.setVisibility(View.GONE);
        etEmail.setVisibility(View.GONE);
        etPassword.setVisibility(View.GONE);
        etLogin.setVisibility(View.GONE);

    }

    public  void ShowLobby()
    {
        Button[] gamebuttons = new Button[]{bt1,bt2, bt3, bt4, bt5,bt6, bt7, bt8, bt9 };

        for(Button b : gamebuttons)
        {
          b.setVisibility(View.GONE);
        }

        Button[] lobbybuttons = new Button[]{btnInvite,btnLogin, btnRefresh, btnReg};

        for(Button b : lobbybuttons)
        {
            b.setVisibility(View.VISIBLE);
        }

        etClients.setVisibility(View.VISIBLE);
        etEmail.setVisibility(View.VISIBLE);
        etPassword.setVisibility(View.VISIBLE);
        etLogin.setVisibility(View.VISIBLE);
    }


    public void moveBtn(Object[] args) {

        if ((Integer)args[1] == 0 && (Integer)args[2] == 0) {
            bt1.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 0 && (Integer)args[2] == 1) {
            bt2.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 0 && (Integer)args[2] == 2) {
            bt3.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 1 && (Integer)args[2] == 0) {
            bt4.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 1 && (Integer)args[2] == 1) {
            bt5.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 1 && (Integer)args[2] == 2) {
            bt6.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 2 && (Integer)args[2] == 0) {
            bt7.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 2 && (Integer)args[2] == 1) {
            bt8.setText(args[0].toString());
        }
        else if ((Integer)args[1] == 2 && (Integer)args[2] == 2) {
            bt9.setText(args[0].toString());
        }

    }

    public class Request {
        String Module;
        String Cmd;
        Object Args;
        public Request(String Module, String Cmd, Object Args) {
            this.Module = Module;
            this.Cmd = Cmd;
            this.Args = Args;
        }
    }

}
