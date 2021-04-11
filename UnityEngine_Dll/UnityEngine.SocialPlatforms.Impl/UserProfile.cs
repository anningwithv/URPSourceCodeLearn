using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	public class UserProfile : IUserProfile
	{
		protected string m_UserName;

		protected string m_ID;

		private string m_legacyID;

		protected bool m_IsFriend;

		protected UserState m_State;

		protected Texture2D m_Image;

		private string m_gameID;

		public string userName
		{
			get
			{
				return this.m_UserName;
			}
		}

		public string id
		{
			get
			{
				return this.m_ID;
			}
		}

		[Obsolete("legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead (UnityUpgradable) -> id")]
		public string legacyId
		{
			get
			{
				return this.m_legacyID;
			}
		}

		public string gameId
		{
			get
			{
				return this.m_gameID;
			}
		}

		public bool isFriend
		{
			get
			{
				return this.m_IsFriend;
			}
		}

		public UserState state
		{
			get
			{
				return this.m_State;
			}
		}

		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		public UserProfile()
		{
			this.m_UserName = "Uninitialized";
			this.m_ID = "0";
			this.m_legacyID = "0";
			this.m_IsFriend = false;
			this.m_State = UserState.Offline;
			this.m_Image = new Texture2D(32, 32);
		}

		public UserProfile(string name, string id, bool friend) : this(name, id, friend, UserState.Offline, new Texture2D(0, 0))
		{
		}

		public UserProfile(string name, string id, bool friend, UserState state, Texture2D image) : this(name, id, id, friend, state, image)
		{
		}

		public UserProfile(string name, string teamId, string gameId, bool friend, UserState state, Texture2D image)
		{
			this.m_UserName = name;
			this.m_ID = teamId;
			this.m_gameID = gameId;
			this.m_IsFriend = friend;
			this.m_State = state;
			this.m_Image = image;
		}

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.id,
				" - ",
				this.userName,
				" - ",
				this.isFriend.ToString(),
				" - ",
				this.state.ToString()
			});
		}

		public void SetUserName(string name)
		{
			this.m_UserName = name;
		}

		public void SetUserID(string id)
		{
			this.m_ID = id;
		}

		public void SetLegacyUserID(string id)
		{
			this.m_legacyID = id;
		}

		public void SetUserGameID(string id)
		{
			this.m_gameID = id;
		}

		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		public void SetIsFriend(bool value)
		{
			this.m_IsFriend = value;
		}

		public void SetState(UserState state)
		{
			this.m_State = state;
		}
	}
}
