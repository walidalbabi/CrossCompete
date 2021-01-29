using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;


public class PlayFabLogin : Singleton<PlayFabLogin>
{

    [SerializeField]
    private string userEmail;

    [SerializeField]
    private string userPassword;

    [SerializeField]
    private string userName;

    [SerializeField]
    private GameObject ResetPassBtn;

    [SerializeField]
    private Text RegisterFailedText;
    [SerializeField]
    private Text LoginFailedText;
    [SerializeField]
    private Text RecoveryText;
    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "801AC"; // Please change this value to your own titleId from PlayFab Game Manager
        }

        if (PlayerPrefs.HasKey("EMAIL") && PlayerPrefs.HasKey("PASSWORD"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }

    }

    #region CallBacks
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        UIAppManager.instance.ShowHome();
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        UpdateContactEmail();
        UIAppManager.instance.ShowHome();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        LoginFailedText.text = "Email address: is not valid Password: is not valid";
        ResetPassBtn.SetActive(true);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        RegisterFailedText.text = error.ToString();
    }

    private void FailUpdateCallBack(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnRecoveryEmailSuccess(SendAccountRecoveryEmailResult result)
    {
        RecoveryText.text = "Recovery Email Sended!!";
    }

    private void OnRecoveryEmailFailed(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        RecoveryText.text = "Invalid email Address";
    }
    #endregion CallBacks

    #region Authentication
    public void GetUserEmail(string emaiIn)
    {
        InputField input = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.
            gameObject.GetComponent<InputField>();
        emaiIn = input.text;
        userEmail = emaiIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        InputField input = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.
       gameObject.GetComponent<InputField>();
        passwordIn = input.text;
        userPassword = passwordIn;
    }

    public void GetUserName(string userNameIn)
    {
        InputField input = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.
       gameObject.GetComponent<InputField>();
        userNameIn = input.text;
        userName = userNameIn;
    }

    private void UpdateContactEmail()
    {
        var requestAddInfo = new AddOrUpdateContactEmailRequest { EmailAddress = userEmail };
        PlayFabClientAPI.AddOrUpdateContactEmail(requestAddInfo, result => {

            Debug.Log("Contact Email Updated");
        }, FailUpdateCallBack);
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void OnClickRegister()
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = userName };
        
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);

       
    }

    public void SendCustomAccountRecoveryEmail()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = userEmail,
            TitleId = PlayFabSettings.TitleId,
            EmailTemplateId = "9FAD9924AD36B9FC"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoveryEmailSuccess, OnRecoveryEmailFailed);
    }


    #endregion Authentication

}