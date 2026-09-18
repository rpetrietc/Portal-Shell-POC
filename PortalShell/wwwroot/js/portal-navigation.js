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
                :host {
                    display: block;
                }

                .portal-bar {
                    background: #f5f5f5;
                    border-bottom: 1px solid #d6d6d6;
                }

                .portal-navigation {
                    max-width: 1280px;
                    margin: 0 auto;
                    padding: 0.75rem 1.5rem;

                    display: flex;
                    align-items: center;
                    gap: 0.85rem;

                    font-family:
                        system-ui,
                        -apple-system,
                        BlinkMacSystemFont,
                        "Segoe UI",
                        sans-serif;
                }

                .portal-home-button {
                    width: 2.75rem;
                    height: 2.75rem;

                    display: inline-flex;
                    align-items: center;
                    justify-content: center;

                    background: #26374a;
                    color: #ffffff;

                    border: 0;
                    border-radius: 4px;

                    text-decoration: none;

                    box-shadow:
                        0 1px 2px rgba(0, 0, 0, 0.15);

                    transition:
                        background 150ms ease,
                        box-shadow 150ms ease,
                        transform 100ms ease;
                }

                .portal-home-button:hover {
                    background: #1c578a;

                    box-shadow:
                        0 2px 5px rgba(0, 0, 0, 0.22);
                }

                .portal-home-button:active {
                    transform: translateY(1px);

                    box-shadow:
                        0 1px 2px rgba(0, 0, 0, 0.18);
                }

                .portal-home-button:focus-visible {
                    outline: 3px solid #ffbf47;
                    outline-offset: 2px;
                }

                .portal-home-button svg {
                    width: 1.45rem;
                    height: 1.45rem;

                    stroke: currentColor;
                    stroke-width: 2;
                    stroke-linecap: round;
                    stroke-linejoin: round;

                    fill: none;
                }

                .separator {
                    width: 1px;
                    height: 2rem;
                    background: #b8b8b8;
                }

                .current-app {
                    color: #26374a;
                    font-size: 1.15rem;
                    font-weight: 600;
                    line-height: 1.25;
                }
            </style>

            <div class="portal-bar">
                <nav class="portal-navigation"
                     aria-label="Portal navigation">

                    <a class="portal-home-button"
                       href="${portalUrl}"
                       aria-label="Return to portal home"
                       title="Portal home">

                        <svg viewBox="0 0 24 24"
                             aria-hidden="true"
                             focusable="false">

                            <path d="M3 11.5L12 4l9 7.5"></path>
                            <path d="M5.5 10v10h13V10"></path>
                            <path d="M9.5 20v-6h5v6"></path>

                        </svg>

                    </a>

                    <span class="separator"
                          aria-hidden="true">
                    </span>

                    <span class="current-app">
                        ${currentApp}
                    </span>

                </nav>
            </div>
        `;
    }
}

customElements.define("portal-navigation", PortalNavigation);