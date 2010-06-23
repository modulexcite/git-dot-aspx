namespace GitAspx.Tests {
	using System;
	using System.Net;
	using NUnit.Framework;
	using System.Linq;

	// These tests are largely ported from GRack.
	// They are all in one file to ensure the webserver is only started once (slooow)
	[TestFixture]
	public class MainTests : BaseTest {
		[Test]
		public void Gets_upload_pack_advertisement() {
			var response = Get("/test/info/refs?service=git-upload-pack");
			response.StatusCode.ShouldEqual(HttpStatusCode.OK);
			response.Headers["Content-Type"].ShouldContain("application/x-git-upload-pack-advertisement");

			var body = response.GetString();

			body.SplitOnNewLine()[0].ShouldEqual("001E# service=git-upload-pack");
			body.ShouldContain("0000009514bf0836c3371b740ebad55fbda6223bd7940f20 HEAD");
			body.ShouldContain("multi_ack_detailed");
		}
	}
}