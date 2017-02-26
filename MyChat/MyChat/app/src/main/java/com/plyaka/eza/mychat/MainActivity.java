package com.plyaka.eza.mychat;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.google.gson.Gson;

import org.java_websocket.client.WebSocketClient;
import org.java_websocket.handshake.ServerHandshake;

import java.net.URI;
import java.net.URISyntaxException;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    Button btnLogin, btnReg, btnInvite, btnRefresh;
    EditText etPassword, etLogin;
    private WebSocketClient mWebSocketClient;
    String login;
    String password;
    final Gson gson = new Gson();

    private static final String TAG = "MyActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        connectWebSocket();


        btnLogin = (Button) findViewById(R.id.btnLogin);
        btnReg = (Button) findViewById(R.id.btnReg);
        btnInvite = (Button) findViewById(R.id.btnInvite);
        btnRefresh = (Button) findViewById(R.id.btnRefresh);

        btnLogin.setOnClickListener(this);
        btnReg.setOnClickListener(this);
        btnInvite.setOnClickListener(this);
        btnRefresh.setOnClickListener(this);




    }

private void getText() {
   // login = etLogin.getText().toString();
    //password = etPassword.getText().toString();


}

    @Override
    public void onClick(View v) {

        {
            switch (v.getId()) {
                case R.id.btnLogin:
                    Request content = new Request("Auth", "LogIn",new Object[]{"2","2"});

                    String json = gson.toJson(content);
                    mWebSocketClient.send(json);
                    break;

                case R.id.btnReg:
                    getText();

                    break;

                case R.id.btnInvite:
                    Intent intent = new Intent(this, RoomActivity.class);
                    startActivity(intent);
                    break;
                case R.id.btnRefresh:
                    break;

            }
        }
    }
    private void connectWebSocket() {
        URI uri;
        try {
            uri = new URI("ws://192.168.51.1:8888/");
        } catch (URISyntaxException e) {
            e.printStackTrace();
            etLogin.setText(e.toString());
            return;
        }

        mWebSocketClient = new WebSocketClient(uri) {
            @Override
            public void onOpen(ServerHandshake serverHandshake) {
                //etLogin.setText("Opened");

                Log.i("Websocket", "Opened");

            }

            @Override
            public void onMessage(String s) {
                final String message = s;
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Log.v(TAG, message);
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

    public void sendMessage(View view) {

    }

    public class Request {
        String Module;
        String Cmd;
        Object Args;
        public Request(String module, String cmd, Object args) {
            this.Module = module;
            this.Cmd = cmd;
            this.Args = args;
        }
    }

}
