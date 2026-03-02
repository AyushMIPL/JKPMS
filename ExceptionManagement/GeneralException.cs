using System;
using System.Runtime.Serialization;
using ExceptionManagement;

namespace ExceptionManagementQuickStartSamples
{
	public class GeneralException : BaseApplicationException
	{
		// Default constructor
		public GeneralException() : base()
		{
		}
		// Constructor with exception message
		public GeneralException(string message) : base(message)
		{
		}
		// Constructor with message and inner exception
		public GeneralException(string message, Exception inner) : base(message,inner)
		{
		}
		// Protected constructor to de-serialize data
        protected GeneralException(SerializationInfo info, StreamingContext context)
            : base(info, context)
		{
		}
	}
}