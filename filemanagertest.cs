[Test]
        public async Task ReadFileContentAsync_ShouldReadFileContent()
        {
            // Arrange
            var fileManager = new FileManager();

            // Mock Dns.GetHostAddresses
            var dnsMock = new Mock<IDns>();
            dnsMock.Setup(dns => dns.GetHostAddresses(It.IsAny<string>()))
                .Returns(new IPAddress[] { IPAddress.Parse("127.0.0.1") });

            // Mock NetworkCredential
            var credentialMock = new Mock<INetworkCredential>();
            credentialMock.Setup(credential => credential.Domain).Returns("your_domain");
            credentialMock.Setup(credential => credential.UserName).Returns("your_username");
            credentialMock.Setup(credential => credential.Password).Returns("your_password");

            // Mock WindowsNetworkFileShare
            var windowsNetworkFileShareMock = new Mock<IWindowsNetworkFileShare>();
            windowsNetworkFileShareMock.Setup(share => share.GetInputStreamAsync())
                .ReturnsAsync(new MemoryStream(new byte[] { 1, 2, 3 }));

            // Provide mocks to the fileManager instance
            fileManager.Dns = dnsMock.Object;
            fileManager.NetworkCredentialFactory = _ => credentialMock.Object;
            fileManager.WindowsNetworkFileShareFactory = (path, credential) => windowsNetworkFileShareMock.Object;

            var storagePath = "your_storage_path"; // Replace with your actual storage path
            var filePath = "your_file_path";       // Replace with your actual file path
            var userDomain = "your_domain";         // Replace with your actual domain
            var username = "your_username";          // Replace with your actual username
            var password = "your_password";          // Replace with your actual password
            var logger = new Mock<ILogger>().Object; // Use a mock logger or another implementation

            // Act
            var result = await fileManager.ReadFileContentAsync(storagePath, filePath, userDomain, username, password, logger);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<byte[]>(result);
            Assert.IsTrue(result.Length > 0);

            // Additional assertions based on your specific requirements
        }
