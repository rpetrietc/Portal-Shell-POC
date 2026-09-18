class PortalNavigation extends HTMLElement {
    connectedCallback() {
        // Prevent the component from being initialized more than once.
        if (this.shadowRoot) {
            return;
        }

        const portalUrl =
            this.getAttribute("portal-url") || "https://localhost:7065";

        const currentApp =
            this.getAttribute("current-app") || "Connected service";

        const root = this.attachShadow({ mode: "open" });

        root.innerHTML = `
            <style>
                .portal-navigation {
                    font-family: Arial, sans-serif;
                    border-bottom: 1px solid #ccc;
                    padding: 1rem 2rem;
                    background: #fff;
                }

                .portal-navigation a {
                    margin-right: 1.5rem;
                }

                .current-app {
                    font-weight: bold;
                }
            </style>

            <nav class="portal-navigation"
                 aria-label="Portal navigation">

                <a href="${portalUrl}">
                    Portal home
                </a>

                <span class="current-app">
                    ${currentApp}
                </span>

            </nav>
        `;
    }
}

customElements.define("portal-navigation", PortalNavigation);