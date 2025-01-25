export const environment = {
  nuVersion: "1.0.0",
  production: true,
  baseUrl: "https://localhost:80",
  tokenWhitelistedDomains: [
    new RegExp('localhost:80'),
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Authenticate'),
  ]
};
