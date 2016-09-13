package md5a9691c4e6211ee5d01a6bf602f427a89;


public class ForgotPasswordActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MPA.ForgotPasswordActivity, MPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ForgotPasswordActivity.class, __md_methods);
	}


	public ForgotPasswordActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ForgotPasswordActivity.class)
			mono.android.TypeManager.Activate ("MPA.ForgotPasswordActivity, MPA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
