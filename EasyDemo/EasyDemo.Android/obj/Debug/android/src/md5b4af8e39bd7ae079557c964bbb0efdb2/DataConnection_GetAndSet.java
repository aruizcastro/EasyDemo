package md5b4af8e39bd7ae079557c964bbb0efdb2;


public class DataConnection_GetAndSet
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("EasyDemo.Droid.Models.DataConnection+GetAndSet, EasyDemo.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DataConnection_GetAndSet.class, __md_methods);
	}


	public DataConnection_GetAndSet () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DataConnection_GetAndSet.class)
			mono.android.TypeManager.Activate ("EasyDemo.Droid.Models.DataConnection+GetAndSet, EasyDemo.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);

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
