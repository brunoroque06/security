import requests
from azure import identity

tenant = "<tenant-id>"
client_id = "<client-id>"
scope = "api://<client-id>/trade"

credential = identity.InteractiveBrowserCredential(
    tenant_id=tenant, client_id=client_id
)
token = credential.get_token(scope)

response = requests.get(
    "http://localhost:8080/secret", headers={"Authorization": f"Bearer {token.token}"}
)

print(response.status_code)
print(response.text)
