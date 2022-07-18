using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Utils.Security;
using Xunit;

namespace Domain.Tests.Utils.SecurityTests;
public class PasswordVerfifierTest
{
    [Fact]
    public void Verify_ShouldBeTrue()
    {
        var expected = "58ee99ab3b809689998962df3699789b6b9bbde660809cb1571199e9376b4264"; // SHA256 hash of abc123salt
        var hasher = PasswordHasherFactory.SHA256();
        Assert.True(PasswordVerifier.Verify("abc123", expected, "salt", hasher));
    }
}
