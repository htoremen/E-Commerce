﻿namespace Identity.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        UserRegisterStages = new List<UserRegisterStage>();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    // Hatalı giriş Sayısı
    public int IncorrectEntry { get; set; }
    public LoginType LoginType { get; set; }
    public DateTime? NextTime {  get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public UserSocialAccount UserSocialAccount { get; set; }
    [JsonIgnore]
    public UserForgotPassword UserForgotPassword { get; set; }

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; set; }
  
    public virtual ICollection<UserNotification> UserNotifications { get; set; }
    public virtual ICollection<UserRegisterStage> UserRegisterStages { get; set; }

}